using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class PhotoRatingModel
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }

        public int UserId { get; set; }

        public double Grade { get; set; }

        public DateTime RatingDate { get; set; }
    }
}
