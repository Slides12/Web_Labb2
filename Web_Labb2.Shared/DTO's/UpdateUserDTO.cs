using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Labb2.Shared.DTO_s
{
    public class UpdateUserDTO
    {
        
        public required string Username { get; set; }


        public string Role { get; set; } = "User";

        
        public string Email { get; set; } = "";
    }

}
