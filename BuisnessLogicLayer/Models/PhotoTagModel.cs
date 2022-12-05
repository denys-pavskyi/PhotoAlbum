using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class PhotoTagModel
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }

        public int TagId { get; set; }

    }
}
