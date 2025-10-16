using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_LeumitCommimentTypes")]
    public class LeumitCommimentTypes
    {
        [StringLength(5)]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }

        public bool IsActive { get; set; }
    }
}


