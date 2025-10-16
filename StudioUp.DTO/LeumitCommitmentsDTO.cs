using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class LeumitCommitmentsDTO
    {

        public string Id { get; set; }
        public int CommitmentTypeId { get; set; }
        public int CustomerId { get; set; }
        public string CommitmentTz { get; set; }
        public DateOnly BirthDate { get; set; }
        public int? FileUploadId { get; set; }
        public DateOnly Validity { get; set; }
        public bool IsActive { get; set; }
    }
}
