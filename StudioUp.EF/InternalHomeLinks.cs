using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioUp.Models
{
    public class InternalHomeLinks
    {
        public int ID { get; set; }

        public string? Title { get; set; }

        public string? Link { get; set; }
        public bool? IsExternal { get; set; }
        public bool? IsActive { get; set; }

    }
}
