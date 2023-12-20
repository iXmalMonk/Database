namespace Program.Models
{
    public class TimetableOfClassesModel
    {
        public List<TOCModel> TOC { get; set; }
        public List<TimeModel> Time { get; set; }
        public List<DayOfTheWeekModel> DayOfTheWeek { get; set; }
    }
    public class TOCModel
    {
        public int Id { get; set; }
        public int IdTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int IdDayOfTheWeek { get; set; }
        public string Title { get; set; }
    }
}
