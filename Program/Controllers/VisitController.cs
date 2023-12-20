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
            List<VisitModel> visitList = new List<VisitModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from fc.visit", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var visit = new VisitModel
                    {
                        Id = reader.GetInt32(1),
                        DateArrival = reader.GetString(2),
                        DateOfDeparture = reader.GetString(3),
                        ClientId = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
                        TrainerId = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0
                    };
                    visitList.Add(visit);
                }
                connection.Close();
            }
            return View(visitList);
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
