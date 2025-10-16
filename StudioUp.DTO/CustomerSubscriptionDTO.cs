namespace StudioUp.DTO
{
    public class CustomerSubscriptionDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int SubscriptionTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }


    }
}
