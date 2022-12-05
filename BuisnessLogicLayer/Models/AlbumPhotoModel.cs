using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class AlbumPhotoModel
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public int PhotoId { get; set; }

        public DateTime AdditionDate { get; set; }
    }
}
