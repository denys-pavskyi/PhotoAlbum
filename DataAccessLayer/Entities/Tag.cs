using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Tag: BaseEntity
    {
        [Required,  StringLength(30)]
        public string Title { get; set; }

        public ICollection<PhotoTag> PhotoTags { get; set; }
    }
}
