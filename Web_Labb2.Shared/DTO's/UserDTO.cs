using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Labb2.Shared.DTO_s
{
    public class UserDTO
    {
        [Required]
        [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
        public required string Username { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; } = "User";
        [Required]
        [StringLength(30, ErrorMessage = "Email is required.", MinimumLength = 8)]
        public string Email { get; set; } = "";
    }

}
