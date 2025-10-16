using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class SubscriptionTypeDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int TotalTraining { get; set; }
        public float PriceForTraining{ get; set; }
        public int NumberOfTrainingPerWeek{ get; set; }
        public string Description { get; set; }

    }
}
