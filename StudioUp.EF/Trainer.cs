using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace StudioUp.Models
{
    [Table("T_Trainers")]
    public class Trainer
    {
     
        public int ID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        [StringLength(20)]
        public string Mail { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        
        [StringLength(50)]
        public string? Address { get; set; }

    }
}
