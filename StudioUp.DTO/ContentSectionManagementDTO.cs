using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.DTO
{
    public class ContentSectionManagementDTO
    {
        public int ID { get; set; }
        public int ContentTypeID { get; set; }
        public string Section1 { get; set; }
        public string Section2 { get; set; }
        public string Section3 { get; set; }
        public bool IsActive { get; set; }
        public bool ViewInHP { get; set; }
        public FileUploadDTO? fileUploadDTO{ get; set; }
    }
}
