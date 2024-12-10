namespace API.Model.DTO
{
    public class RegisterRequestDto
    {
        public string? Email { get; set; }
        public string Role { get; set; } = "user";
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class UserProfileDto
    {
        public Guid Id { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public bool IsActive { get; set; }   
    }

    public class UpdateProfileRequestDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; }
        public bool? IsActive { get; set; } 
    }
    public class UpdateActiveStatusDto
    {
        public bool IsActive { get; set; }
    }
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }




}
