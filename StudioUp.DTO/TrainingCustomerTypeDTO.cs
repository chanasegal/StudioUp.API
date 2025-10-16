using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class TrainingCustomerTypeDTO
    {
        public int ID { get; set; }
        public int CustomerTypeID { get; set; }
        public int TrainingTypeID { get; set; }
        public string TrainingCustomerTypeName { get; set; }
        public bool IsActive { get; set; }


    }
}
