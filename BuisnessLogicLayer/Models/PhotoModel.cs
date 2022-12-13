using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }

        public string PhotoPath { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime UploadDate { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public double? TotalRating { get; set; }


        public ICollection<int>? AlbumPhotoIds { get; set; }
        public ICollection<int>? PhotoRatingIds { get; set; }
        public ICollection<int>? PhotoTagsIds { get; set; }

    }
}
