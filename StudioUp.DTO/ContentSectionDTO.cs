namespace StudioUp.DTO
{
    public class ContentSectionDTO
    {
        public int ID { get; set; }
        public int ContentTypeID { get; set; }
        public string Section1 { get; set; }
        public string Section2 { get; set; }
        public string Section3 { get; set; }
        public FileDownloadDTO? Image { get; set; }
        public bool IsActive { get; set; }
        public bool ViewInHP { get; set; }
    }
}
