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

        public string? PhotoUrl { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? UploadDate { get; set; }

        public int UserId { get; set; }

        public double? TotalRating { get; set; }


        public ICollection<int>? AlbumPhotoIds { get; set; }
        public ICollection<int>? PhotoRatingIds { get; set; }
        public ICollection<int>? PhotoTagsIds { get; set; }

    }
}
