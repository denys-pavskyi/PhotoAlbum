using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string PasswordSalt { get; set; }

        public DateTime BirthDate { get; set; }


        public DateTime RegistrationDate { get; set; }

        public Role Role { get; set; }
        public UserStatus UserStatus { get; set; }

        public ICollection<int>? PhotoRatingIds { get; set; }
        public ICollection<int>? PhotoIds { get; set; }
        public ICollection<int>? AlbumIds { get; set; }

    }
}
