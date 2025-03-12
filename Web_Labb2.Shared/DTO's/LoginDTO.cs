using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Labb2.Shared.DTO_s
{
    public class LoginDTO
    {
        [Required]
        [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
        public required string Username { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Password is at least 8 characters long.", MinimumLength = 8)]
        public required string Password { get; set; }
    }
}
