﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Photo: BaseEntity
    {
        [Required, StringLength(200)]
        
        public string PhotoPath { get; set; }

        [Required, StringLength(150)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        public double TotalRating { get; set; }

        [AllowNull]
        public User User { get; set; }
        public ICollection<AlbumPhoto> AlbumPhotos { get; set; }
        public ICollection<PhotoRating> PhotoRatings { get; set; }
        public ICollection<PhotoTag> PhotoTags { get; set; }

    }
}
