using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Program.Models;

namespace Program.Controllers
{
    public class SubscriptionController : Controller
    {
        public IActionResult Index()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            SubscriptionModel subscription = new SubscriptionModel();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select distinct s.id, s.visit, c.full_name, g.title from fc.subscription s join fc.client_subscription cs on s.id = cs.subscription_id join fc.client c on cs.client_id = c.id join fc.client_group cg on c.id = cg.client_id join fc.group g on cg.group_id = g.id", connection);
                var reader = command.ExecuteReader();
                subscription.S = new List<SModel>();
                while (reader.Read())
                {
                    var S = new SModel
                    {
                        Id = reader.GetInt32(0),
                        Visit = reader.GetInt32(1),
                        ClientFullName = reader.GetString(2),
                        GroupTitle = reader.GetString(3)
                    };
                    subscription.S.Add(S);
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
            subscription.Group = group;
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
            subscription.Client = clientList;
            return View(subscription);
        }

        public IActionResult Insert(int clientId, int groupId, int visit)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("call fc.client_group_subscription_procedure_insert(@client_id, @group_id, @visit)", connection);
                command.Parameters.AddWithValue("client_id", clientId);
                command.Parameters.AddWithValue("group_id", groupId);
                command.Parameters.AddWithValue("visit", visit);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id, int visit)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("call fc.subscription_procedure_update(@id, @visit)", connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("visit", visit);
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
                var command = new NpgsqlCommand("delete from fc.subscription where id = @id", connection);
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
