using StudioUp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace StudioUp.Models
{
    [Table("T_TrainigTypes")]
    public class TrainingType
    {
        
        public int ID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public bool IsActive { get; set; }

    }
}

