namespace Program.Models
{
    public class VisitModel
    {
        public List<VModel> V { get; set; }
        public List<ClientModel> Client { get; set; }
        public TrainerModel Trainer { get; set; }
    }

    public class VModel
    {
        public int Id { get; set; }
        public string DateArrival { get; set; }
        public string DateOfDeparture { get; set; }
        public string ClientFullName { get; set; }
        public string TrainerFullName { get; set; }
    }
}
