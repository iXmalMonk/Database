namespace Program.Models
{
    public class GroupModel
    {
        public List<GModel> G { get; set; }
        public TimetableOfClassesModel TimetableOfClasses { get; set; }
        public List<TypeOfOccupationModel> TypeOfOccupation { get; set; }
    }
    public class GModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TimetableOfClassesId { get; set; }
        public int TypeOfOccupationId { get; set; }
    }
}
