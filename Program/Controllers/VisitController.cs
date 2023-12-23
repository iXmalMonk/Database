using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Program.Models;

namespace Program.Controllers
{
    public class VisitController : Controller
    {
        public IActionResult Index()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            VisitModel visit = new VisitModel();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select v.id, v.date_arrival, v.date_of_departure, c.full_name, t.full_name from fc.visit v " +
                    "left join fc.client c on v.client_id = c.id " +
                    "left join fc.trainer t on v.trainer_id = t.id", connection);
                var reader = command.ExecuteReader();
                visit.V = new List<VModel>();
                while (reader.Read())
                {
                    var V = new VModel
                    {
                        Id = reader.GetInt32(0),
                        DateArrival = reader.GetString(1),
                        DateOfDeparture = reader.GetString(2),
                        ClientFullName = !reader.IsDBNull(3) ? reader.GetString(3) : "-",
                        TrainerFullName = !reader.IsDBNull(4) ? reader.GetString(4) : "-"
                    };
                    visit.V.Add(V);
                }
                connection.Close();
            }
            List<ClientModel> clientList = new List<ClientModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select c.id, c.full_name from fc.client c", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var client = new ClientModel
                    {
                        Id = reader.GetInt32(0),
                        FullName = reader.GetString(1)
                    };
                    clientList.Add(client);
                }
                connection.Close();
            }
            visit.Client = clientList;
            TrainerModel trainer = new TrainerModel();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select t.id, t.full_name, g.title from fc.trainer t join fc.group g on t.group_id = g.id", connection);
                var reader = command.ExecuteReader();
                trainer.T = new List<TModel>();
                while (reader.Read())
                {
                    var T = new TModel
                    {
                        Id = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        GroupTitle = reader.GetString(2)
                    };
                    trainer.T.Add(T);
                }
                connection.Close();
            }
            GroupModel group = new GroupModel();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select g.id, g.title, g.timetable_of_classes_id, g.type_of_occupation_id from fc.group g", connection);
                var reader = command.ExecuteReader();
                group.G = new List<GModel>();
                while (reader.Read())
                {
                    var G = new GModel
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        TimetableOfClassesId = reader.GetInt32(2),
                        TypeOfOccupationId = reader.GetInt32(3)
                    };
                    group.G.Add(G);
                }
                connection.Close();
            }
            TimetableOfClassesModel timetableOfClasses = new TimetableOfClassesModel();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select toc.id, dotw.title, t.start_time, t.end_time from fc.timetable_of_classes toc " +
                    "join fc.day_of_the_week dotw on toc.day_of_the_week_id = dotw.id " +
                    "join fc.time t on toc.time_id = t.id", connection);
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
            group.TimetableOfClasses = timetableOfClasses;
            List<TypeOfOccupationModel> typeOfOccupationList = new List<TypeOfOccupationModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select too.id, too.title from fc.type_of_occupation too", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var typeOfOccupation = new TypeOfOccupationModel
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1)
                    };
                    typeOfOccupationList.Add(typeOfOccupation);
                }
                connection.Close();
            }
            group.TypeOfOccupation = typeOfOccupationList;
            trainer.Group = group;
            visit.Trainer = trainer;
            return View(visit);
        }

        public IActionResult Insert(string dateArrival, string dateOfDeparture, int clientId, int trainerId)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("insert into fc.visit (date_arrival, date_of_departure, client_id, trainer_id) values (@date_arrival, @date_of_departure, @client_id, @trainer_id)", connection);
                command.Parameters.AddWithValue("date_arrival", dateArrival);
                command.Parameters.AddWithValue("date_of_departure", dateOfDeparture);
                command.Parameters.AddWithValue("client_id", clientId == 0 ? DBNull.Value : clientId);
                command.Parameters.AddWithValue("trainer_id", trainerId == 0 ? DBNull.Value : trainerId);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
