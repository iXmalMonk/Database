namespace Program.Models
{
    public class SubscriptionModel
    {
        public List<SModel> S { get; set; }
        public GroupModel Group { get; set; }
        public List<ClientModel> Client { get; set; }
    }

    public class SModel
    {
        public int Id { get; set; }
        public int Visit { get; set; }
        public string ClientFullName { get; set; }
        public string GroupTitle { get; set; }
    }
}
