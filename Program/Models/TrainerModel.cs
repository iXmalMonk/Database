namespace Program.Models
{
    public class TrainerModel
    {
        public List<TModel> T { get; set; }
        public GroupModel Group { get; set; }
    }

    public class TModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string GroupTitle { get; set; }
    }
}
