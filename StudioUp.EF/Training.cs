using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StudioUp.DTO;
using System.Diagnostics.CodeAnalysis;

namespace StudioUp.Models
{
    [Table("T_Trainings")]
    public class Training
    {
        public int ID { get; set; }

        [ForeignKey("TrainingCustomerType")]
        public int? TrainingCustomerTypeId { get; set; }
        public virtual TrainingCustomerType TrainingCustomerType { get; set; }
        
        [ForeignKey("Trainers")]
        public int? TrainerID { get; set; }
        public virtual Trainer Trainer { get; set; }
        public int DayOfWeek { get; set; }

        [Range(0, 23)]
        public int Hour { get; set; }

        [Range(0, 59)]
        public int Minute { get; set; }

        public int? ParticipantsCount { get; set; }
        public bool IsActive { get; set; }



    }
}
