namespace Hackathon.Dto
{
    public class UserUpdateDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool ChangePassword { get; set; }
    }
}
