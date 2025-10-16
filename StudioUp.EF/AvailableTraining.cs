using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace StudioUp.Models
{
    [Table("T_AvailableTrainings")]
    public class AvailableTraining
    { 
        public int Id {  get; set; }
        [ForeignKey("Trainings")]
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }
        public DateTime Date {  get; set; }
        //public int Tonnage { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsActive { get; set; }

    }
}
