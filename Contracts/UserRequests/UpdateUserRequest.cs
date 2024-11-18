using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Contracts
{
    public class UpdateUserRequest
    {
        [StringLength(20)]
        public string? Password { get; set; }

        [StringLength(20)]
        public string? Nickname { get; set; }
    }
}