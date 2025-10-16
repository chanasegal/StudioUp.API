using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_TrainingCustomerTypes")]
    public class TrainingCustomerType
    {
        public int Id { get; set; }
        [ForeignKey("CustomerTypes")]
        public int CustomerTypeID { get; set; }
        public virtual CustomerType CustomerType { get; set; }

        [ForeignKey("TrainingTypes")]
        public int TrainingTypeId { get; set; }
        public virtual TrainingType TrainingType { get; set; }
        public bool IsActive { get; set; }
    }
}
