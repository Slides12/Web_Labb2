using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Labb2.Shared.DTO_s
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
