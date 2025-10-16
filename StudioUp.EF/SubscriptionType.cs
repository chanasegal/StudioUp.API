using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_SubscriptionTypes")]
    public class SubscriptionType
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string? Title { get; set; }
        public bool IsActive { get; set; }
        
        public int? TotalTraining { get; set; }
        
        public float? PriceForTraining { get; set; }
        
        public int? NumberOfTrainingPerWeek { get; set; }
        
        public string? Description { get; set; }

    }
}
