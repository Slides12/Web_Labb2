using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Labb2.Shared.DTO_s
{
    public class AuthResponse
    {
        public string? Username { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}
