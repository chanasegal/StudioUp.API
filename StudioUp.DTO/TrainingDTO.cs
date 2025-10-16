using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class TrainingDTO
    {
        private string hour = "00";
        private string minute = "00";
        public int ID { get; set; }
        //public int TrainingTypeID { get; set; }
        //public int CustomerTypeID { get; set; }
       // public int TrainingCustomerTypeID {  get; set; }
        public int TrainerID { get; set; }
        public int DayOfWeek { get; set; }

        public string TrainerName { get; set; }
        public string CustomerTypeName { get; set; }
        public string TrainingTypeName { get; set; }
      


        public string Hour
        {
            get { return hour; }
            set { hour = FormatToTwoDigits(value); }
        }

        public string Minute
        {
            get { return minute; }
            set { minute = FormatToTwoDigits(value); }
        }

        
        public int TrainingCustomerTypeId { get; set; }

        public string TrainingCustomerTypeName { get; set; }
        public int ParticipantsCount { get; set; }
        public bool IsActive { get; set; }


        private string FormatToTwoDigits(string value)
        {
            if (int.TryParse(value, out int number) && number >= 0 && number <= 59)
            {
                return number.ToString("D2");
            }
            return "11";
        }


    }
}
