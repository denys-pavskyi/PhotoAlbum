using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class AlbumModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty; 

        public string? Description { get; set; }

        public int? NumberOfPictures { get; set; }

        public int UserId { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<int>? AlbumPhotoIds { get; set; }


    }
}
