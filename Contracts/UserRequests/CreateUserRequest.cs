using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Contracts
{
    public class CreateUserRequest
    {
        [Required]
        [StringLength(20)]
        public string? Login { get; set; }

        [Required]
        [StringLength(20)]
        public string? Password { get; set; }

        [Required]
        [StringLength(20)]
        public string? Nickname { get; set; }
    }
}