namespace Program.Models
{
    public class TimetableOfClassesModel
    {
        public List<TOCModel> TOC { get; set; }
        public List<DayOfTheWeekModel> DayOfTheWeek { get; set; }
        public List<TimeModel> Time { get; set; }
    }

    public class TOCModel
    {
        public int Id { get; set; }
        public string DayOfTheWeekTitle { get; set; }
        public string TimeStartTime { get; set; }
        public string TimeEndTime { get; set; }
    }
}
