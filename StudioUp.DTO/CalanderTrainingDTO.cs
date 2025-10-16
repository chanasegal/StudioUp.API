using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class CalanderTrainingDTO
    {
        public int ID { get; set; }
        public int TrainerID { get; set; }
        public string TrainerName { get; set; }
        public string CustomerTypeName { get; set; }
        public int TrainingTypeId {  get; set; }
        public string TrainingTypeName { get; set; }
        public int DayOfWeek { get; set; }
        public string Hour { get; set; }
        public bool IsActive { get; set; }

    }
}
