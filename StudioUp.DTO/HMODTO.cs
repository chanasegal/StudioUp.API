using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class HMODTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string ArrangementName { get; set; }
        public int TrainingsPerMonth { get; set; }
        public double TrainingPrice { get; set; }
        public double MinimumAge { get; set; }
        public double MaximumAge { get; set; }
        public string TrainingDescription { get; set; }


    }
}
