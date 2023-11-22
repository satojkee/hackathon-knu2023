namespace Hackathon.Models
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}