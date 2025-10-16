using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Tz { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustomerTypeId { get; set; }
        public int HMOId { get; set; }
        public int PaymentOptionId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public bool IsActive { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
