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
    [Table("T_HMOs")]
    public class HMO
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public bool IsActive { get; set; }
        
        public string? ArrangementName { get; set; }
        
        public int? TrainingsPerMonth { get; set; }
        
        public double? TrainingPrice { get; set; }
        
        public double? MinimumAge { get; set; }
        
        public double? MaximumAge { get; set; }
        
        public string? TrainingDescription { get; set; }
    }
}
