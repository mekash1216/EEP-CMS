using API.Model;
using API.Model.DTO;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.roleManager = roleManager;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
          
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Fetch roles of the user
            var roles = await userManager.GetRolesAsync(user);

        
            var claims = await userManager.GetClaimsAsync(user);
            var firstName = claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
            var lastName = claims.FirstOrDefault(c => c.Type == "LastName")?.Value;


            var userData = new
            {
                FirstName = firstName,
                LastName = lastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roles,
                IsActive = user.IsActive 
            };

            return Ok(userData);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email is required.");
            }

            var roleExists = await roleManager.RoleExistsAsync(request.Role);
            if (!roleExists)
            {
                return BadRequest($"Role '{request.Role}' does not exist.");
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsActive = request.IsActive
            };

            // Set the default password here
            var defaultPassword = "Eep@123";
            var result = await userManager.CreateAsync(user, defaultPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, request.Role);
                await userManager.AddClaimsAsync(user, new List<Claim>
        {
            new Claim("FirstName", request.FirstName ?? string.Empty),
            new Claim("LastName", request.LastName ?? string.Empty),
            new Claim(ClaimTypes.Email, request.Email ?? string.Empty),
        });

                return Ok("User registered successfully.");
            }

            return BadRequest(result.Errors);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            if (!user.IsActive)
            {
                return Unauthorized("Your account is not active.");
            }

            if (await userManager.CheckPasswordAsync(user, request.Password))
            {
                var roles = await userManager.GetRolesAsync(user);

                var firstName = (await userManager.GetClaimsAsync(user))
            .FirstOrDefault(c => c.Type == "FirstName")?.Value;
                var lastName = (await userManager.GetClaimsAsync(user))
                    .FirstOrDefault(c => c.Type == "LastName")?.Value;
                var token = tokenRepository.CreateJwtToken(user, roles.ToList());
                return Ok(new { Token = token, Roles = roles, Email = user.Email,
                    FirstName = firstName, 
                    LastName = lastName
                });
            }

            return Unauthorized("Invalid credentials");
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfiles()
        {
            var users = await userManager.Users.ToListAsync();
            var userProfiles = new List<UserProfileDto>();

            foreach (var user in users)
            {
                var claims = await userManager.GetClaimsAsync(user);
                var roles = await userManager.GetRolesAsync(user);

                userProfiles.Add(new UserProfileDto
                {
                    Id = Guid.Parse(user.Id),
                    FirstName = claims.FirstOrDefault(c => c.Type == "FirstName")?.Value,
                    LastName = claims.FirstOrDefault(c => c.Type == "LastName")?.Value,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles.ToList(),
                    IsActive = user.IsActive 
                });
            }

            return Ok(userProfiles);
        }
        [HttpPost("update-profile/{id}")]
        public async Task<IActionResult> UpdateUserProfile(Guid id, [FromBody] UpdateProfileRequestDto request)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }


            // Update fields
            if (!string.IsNullOrEmpty(request.Email))
            {
                // Update the username to match the new email
                user.UserName = request.Email;
                user.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                user.PhoneNumber = request.PhoneNumber;
            }

            if (request.IsActive.HasValue)
            {
                user.IsActive = request.IsActive.Value;
            }

            // Update user in the database
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
            }

            // Update claims
            var claims = await userManager.GetClaimsAsync(user);

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                var firstNameClaim = claims.FirstOrDefault(c => c.Type == "FirstName");
                if (firstNameClaim != null)
                {
                    await userManager.RemoveClaimAsync(user, firstNameClaim);
                }
                await userManager.AddClaimAsync(user, new Claim("FirstName", request.FirstName));
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                var lastNameClaim = claims.FirstOrDefault(c => c.Type == "LastName");
                if (lastNameClaim != null)
                {
                    await userManager.RemoveClaimAsync(user, lastNameClaim);
                }
                await userManager.AddClaimAsync(user, new Claim("LastName", request.LastName));
            }

            return Ok(new { message = "Profile updated successfully." });
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roles = await userManager.GetRolesAsync(user);
            var claims = await userManager.GetClaimsAsync(user);
            var firstName = claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
            var lastName = claims.FirstOrDefault(c => c.Type == "LastName")?.Value;

            var userData = new
            {
                FirstName = firstName,
                LastName = lastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roles,
                IsActive = user.IsActive
            };

            return Ok(userData);
        }


        [HttpPost("update-active-status/{id}")]
        public async Task<IActionResult> UpdateActiveStatus(Guid id, [FromBody] UpdateActiveStatusDto request)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.IsActive = request.IsActive;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "User status updated successfully." });
        }
        [HttpDelete("delete/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { errors = result.Errors });
            }

            return Ok(new { message = "User deleted successfully." });
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var user = await userManager.FindByEmailAsync(changePasswordDto.Email);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            if (!await userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword))
            {
                return BadRequest(new { message = "Old password is incorrect." });
            }

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                return BadRequest(new { message = "New password and confirmation do not match." });
            }

            var result = await userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new { message = "Password changed successfully." }); 
            }

            return BadRequest(new { message = "Error changing password.", errors = result.Errors.Select(e => e.Description) });
        }


    }
}
