namespace StudioUp.DTO
{
    public class CustomerFixedTrainingDTO
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? TrainingId { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
