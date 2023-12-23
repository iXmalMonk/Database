using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Program.Models;

namespace Program.Controllers
{
    public class TimetableOfClassesController : Controller
    {
        public IActionResult Index()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            TimetableOfClassesModel timetableOfClasses = new TimetableOfClassesModel();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select toc.id, dotw.title, t.start_time, t.end_time from fc.timetable_of_classes toc join fc.day_of_the_week dotw on toc.day_of_the_week_id = dotw.id join fc.time t on toc.time_id = t.id", connection);
                var reader = command.ExecuteReader();
                timetableOfClasses.TOC = new List<TOCModel>();
                while (reader.Read())
                {
                    var TOC = new TOCModel
                    {
                        Id = reader.GetInt32(0),
                        DayOfTheWeekTitle = reader.GetString(1),
                        TimeStartTime = reader.GetString(2),
                        TimeEndTime = reader.GetString(3)
                    };
                    timetableOfClasses.TOC.Add(TOC);
                }
                connection.Close();
            }
            List<DayOfTheWeekModel> dayOfTheWeekList = new List<DayOfTheWeekModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select dotw.id, dotw.title from fc.day_of_the_week dotw", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var dayOfTheWeek = new DayOfTheWeekModel
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1)
                    };
                    dayOfTheWeekList.Add(dayOfTheWeek);
                }
                connection.Close();
            }
            timetableOfClasses.DayOfTheWeek = dayOfTheWeekList;
            List<TimeModel> timeList = new List<TimeModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select t.id, t.start_time, t.end_time from fc.time t", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var time = new TimeModel
                    {
                        Id = reader.GetInt32(0),
                        StartTime = reader.GetString(1),
                        EndTime = reader.GetString(2)
                    };
                    timeList.Add(time);
                }
                connection.Close();
            }
            timetableOfClasses.Time = timeList;
            return View(timetableOfClasses);
        }

        public IActionResult Insert(int idDayOfTheWeek, int idTime)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("insert into fc.timetable_of_classes (day_of_the_week_id, time_id) values (@day_of_the_week_id, @time_id)", connection);
                command.Parameters.AddWithValue("day_of_the_week_id", idDayOfTheWeek);
                command.Parameters.AddWithValue("time_id", idTime);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id, int idDayOfTheWeek, int idTime)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("update fc.timetable_of_classes set day_of_the_week_id = @day_of_the_week_id, time_id = @time_id where id = @id", connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("day_of_the_week_id", idDayOfTheWeek);
                command.Parameters.AddWithValue("time_id", idTime);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("delete from fc.timetable_of_classes where id = @id", connection);
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
