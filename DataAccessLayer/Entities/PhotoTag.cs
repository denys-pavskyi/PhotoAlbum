using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class PhotoTag: BaseEntity
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PhotoId { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TagId { get; set; }



        public Photo Photo { get; set; }
        public Tag Tag { get; set; }
    }
}
