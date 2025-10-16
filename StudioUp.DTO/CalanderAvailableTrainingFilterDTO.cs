using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class CalanderAvailableTrainingFilterDTO
    {
        public bool? Past { get; set; }
        public bool? Future { get; set; }
        public DateOnly? StratDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
