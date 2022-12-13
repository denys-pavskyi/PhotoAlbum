using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class LoginResponse
    {
        public string Username { get; set; } = string.Empty;


        public string FirstName { get; set; } = string.Empty;


        public string LastName { get; set; } = string.Empty;

        public int? Id { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}
