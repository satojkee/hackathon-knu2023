using System.ComponentModel.DataAnnotations;

namespace Hackathon.Dto
{
    public class UserCreateDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
