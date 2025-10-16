    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class CalanderAvailableTrainingDTO
    {
        //public int Id { get; set; }
        public int TrainingId { get; set; }
        public string  TrainerName { get; set; }
        public DateOnly Date { get; set; }
        public int DayOfWeek { get; set; }
        public string Time { get; set; }
        public string CustomerTypeName { get; set; }
        public string TrainingTypeName { get; set; }
        public int ParticipantsCount { get; set; } = 0;
        public bool Attended { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsActive { get; set; }   

    }
}
