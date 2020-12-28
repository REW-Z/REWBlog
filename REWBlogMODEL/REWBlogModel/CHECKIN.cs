using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REWBlogModel
{
    public class CHECKIN
    {
        [Key]
        public int CheckID { get; set; }

        [Required]
        public string USERNAME { get; set; }

        [Required]
        public DateTime LASTCHECKIN { get; set; }
    }
}
