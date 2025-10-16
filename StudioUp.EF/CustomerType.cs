using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudioUp.Models
{
    [Table("T_CustomerTypes")]
    public class CustomerType
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public bool IsActive { get; set; }

    }
}
