using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User : BaseEntity
    {

        [Required, StringLength(40)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required ,EmailAddress]
        public string EmailAddress { get; set; }    

        [Required, StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }


        [Required]
        public DateTime RegistrationDate { get; set; }


        [Required]
        public Role Role { get; set; }

        [Required]
        public UserStatus UserStatus { get; set; }

        [Required]


        public ICollection<PhotoRating> PhotoRatings { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Album> Albums { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
     
    public enum Role
    {
        Admin,
        User
    }

    public enum UserStatus
    {
        Active,
        Banned
    }
    
}
