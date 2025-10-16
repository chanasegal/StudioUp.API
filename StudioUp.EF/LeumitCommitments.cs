using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Models
{
    [Table("T_LeumitCommitments")]
    public class LeumitCommitments
    {
        [StringLength(9)]
        public string Id { get; set; }

        
        [ForeignKey("LeumitCommimentTypes")]
        public int? CommitmentTypeId { get; set; }
        public virtual LeumitCommimentTypes LeumitCommimentTypes { get; set; }

        
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
     
        [StringLength(9)]
        public string CommitmentTz { get; set; }
        public DateOnly BirthDate { get; set; }
        
        [ForeignKey("FileUpload")]
        public int? FileUploadId { get; set; }
        public virtual FileUpload FileUpload { get; set; }
        public DateOnly Validity { get; set; }

        public bool IsActive { get; set; }  
    }
}
