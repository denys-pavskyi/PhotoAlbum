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
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Age { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }


        public ICollection<PhotoRating> PhotoRatings { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
