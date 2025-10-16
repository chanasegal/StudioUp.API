using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class TrainingCustomerDTO
    {
        public int ID { get; set; }
        public int TrainingID { get; set; }
        public int CustomerID { get; set; }
        public bool Attended { get; set; }
        public bool IsActive { get; set; }
        public string TrainingName { get; set; }
        public string TrainerName {  get; set; }
        public DateOnly TrainingDate { get; set; }

    }
}
