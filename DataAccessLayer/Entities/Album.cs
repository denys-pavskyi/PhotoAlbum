
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Album: BaseEntity
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public int NumberOfPictures { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        
        
        public User User { get; set; }
        public ICollection<AlbumPhoto> AlbumPhotos { get; set; }
    }
}
