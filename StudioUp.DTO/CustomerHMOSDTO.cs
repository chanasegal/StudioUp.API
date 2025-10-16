using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class CustomerHMOSDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int HMOID { get; set; }
        public string FreeFitId { get; set; }
        public int FiledId { get; set; }
        public bool IsActive { get; set; }

    }
}
