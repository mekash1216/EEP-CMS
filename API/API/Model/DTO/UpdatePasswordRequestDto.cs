namespace API.Model.DTO
{
    public class UpdatePasswordRequestDto
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
