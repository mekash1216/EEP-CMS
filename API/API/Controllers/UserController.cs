using API.Data;
using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext dbContext;
        public UserController(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        //[Authorize] 
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = dbContext.Users.ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult AddUsers(CreateUserDTO request)
        {
            var modeluser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Role = request.Role,
                Password = request.Password,
                RegistrationDate = DateTime.UtcNow
            };
            dbContext.Users.Add(modeluser);
            dbContext.SaveChanges();
            return Ok(modeluser);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteUsers(Guid id)
        {
            var user = dbContext.Users.Find(id);
            if(user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
            return Ok();
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Updateuser(Guid id,CreateUserDTO updateUserDTo)
        {
            var user = dbContext.Users.Find(id);
            if(user is null)
            {
                return NotFound();
            }
            user.FirstName = updateUserDTo.FirstName;
            user.LastName = updateUserDTo.LastName;
            user.Email = updateUserDTo.Email;
            user.Gender = updateUserDTo.Gender;
            user.DateOfBirth = updateUserDTo.DateOfBirth;
            user.Role = updateUserDTo.Role;
            dbContext.SaveChanges();
            return Ok();

        }
    }
}
