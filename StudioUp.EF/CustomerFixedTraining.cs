using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_CustomerFixedTrainings")]
    public class CustomerFixedTraining
    {
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Training")]
        public int? TrainingId { get; set; }
        public virtual Training Training { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
