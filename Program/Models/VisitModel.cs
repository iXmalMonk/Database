namespace Program.Models
{
    public class VisitModel
    {
        public int Id { get; set; }
        public string DateArrival { get; set; }
        public string DateOfDeparture { get; set; }
        public int? ClientId { get; set; }
        public int? TrainerId { get; set; }
    }
}
