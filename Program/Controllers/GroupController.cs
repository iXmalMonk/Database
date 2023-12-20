using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Program.Models;

namespace Program.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            GroupModel group = new GroupModel();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from fc.group", connection);
                var reader = command.ExecuteReader();
                group.G = new List<GModel>();
                while (reader.Read())
                {
                    var g = new GModel
                    {
                        Id = reader.GetInt32(1),
                        Title = reader.GetString(2),
                        TimetableOfClassesId = reader.GetInt32(3),
                        TypeOfOccupationId = reader.GetInt32(4)
                    };
                    group.G.Add(g);
                }
                connection.Close();
            }
            TimetableOfClassesModel timetableOfClasses = new TimetableOfClassesModel();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select fc.timetable_of_classes.id, fc.time.id, fc.time.start_time, fc.time.end_time, fc.day_of_the_week.id, fc.day_of_the_week.title from fc.timetable_of_classes left join fc.time on fc.timetable_of_classes.time_id = fc.time.id left join fc.day_of_the_week on fc.timetable_of_classes.day_of_the_week_id = fc.day_of_the_week.id", connection);
                var reader = command.ExecuteReader();
                timetableOfClasses.TOC = new List<TOCModel>();
                while (reader.Read())
                {
                    var TOC = new TOCModel
                    {
                        Id = reader.GetInt32(0),
                        IdTime = reader.GetInt32(1),
                        StartTime = reader.GetString(2),
                        EndTime = reader.GetString(3),
                        IdDayOfTheWeek = reader.GetInt32(4),
                        Title = reader.GetString(5)
                    };
                    timetableOfClasses.TOC.Add(TOC);
                }
                connection.Close();
            }
            List<TimeModel> timeList = new List<TimeModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from fc.time", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var time = new TimeModel
                    {
                        Id = reader.GetInt32(1),
                        StartTime = reader.GetString(2),
                        EndTime = reader.GetString(3)
                    };
                    timeList.Add(time);
                }
                connection.Close();
            }
            timetableOfClasses.Time = timeList;
            List<DayOfTheWeekModel> dayOfTheWeekList = new List<DayOfTheWeekModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from fc.day_of_the_week", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var dayOfTheWeek = new DayOfTheWeekModel
                    {
                        Id = reader.GetInt32(1),
                        Title = reader.GetString(2)
                    };
                    dayOfTheWeekList.Add(dayOfTheWeek);
                }
                connection.Close();
            }
            timetableOfClasses.DayOfTheWeek = dayOfTheWeekList;
            group.TimetableOfClasses = timetableOfClasses;
            List<TypeOfOccupationModel> typeOfOccupationList = new List<TypeOfOccupationModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from fc.type_of_occupation", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var typeOfOccupation = new TypeOfOccupationModel
                    {
                        Id = reader.GetInt32(1),
                        Title = reader.GetString(2)
                    };
                    typeOfOccupationList.Add(typeOfOccupation);
                }
                connection.Close();
            }
            group.TypeOfOccupation = typeOfOccupationList;
            return View(group);
        }

        public IActionResult Insert(string title, int timetableOfClassesId, int typeOfOccupationId)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("insert into fc.group (title, timetable_of_classes_id, type_of_occupation_id) values (@title, @timetable_of_classes_id, @type_of_occupation_id)", connection);
                command.Parameters.AddWithValue("title", title);
                command.Parameters.AddWithValue("timetable_of_classes_id", timetableOfClassesId);
                command.Parameters.AddWithValue("type_of_occupation_id", typeOfOccupationId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id, string title, int timetableOfClassesId, int typeOfOccupationId)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("update fc.group set title = @title, timetable_of_classes_id = @timetable_of_classes_id, type_of_occupation_id = @type_of_occupation_id where id = @id", connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("title", title);
                command.Parameters.AddWithValue("timetable_of_classes_id", timetableOfClassesId);
                command.Parameters.AddWithValue("type_of_occupation_id", typeOfOccupationId);
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
                var command = new NpgsqlCommand("delete from fc.group where id = @id", connection);
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
