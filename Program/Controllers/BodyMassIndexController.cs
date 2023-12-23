using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Program.Models;

namespace Program.Controllers
{
    public class BodyMassIndexController : Controller
    {
        public IActionResult Index()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            List<BodyMassIndexModel> bodyMassIndexList = new List<BodyMassIndexModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select bmi.id, bmi.weight, bmi.height, bmi.bms, c.full_name from fc.body_mass_index bmi " +
                    "join fc.client c on bmi.id = c.body_mass_index_id", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var bodyMassIndex = new BodyMassIndexModel
                    {
                        Id = reader.GetInt32(0),
                        Weight = reader.GetFloat(1),
                        Height = reader.GetFloat(2),
                        Bms = reader.GetFloat(3),
                        ClientFullName = reader.GetString(4)
                    };
                    bodyMassIndexList.Add(bodyMassIndex);
                }
                connection.Close();
            }
            return View(bodyMassIndexList);
        }

        public IActionResult Update(int id, float weight, float height)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("update fc.body_mass_index set weight = @weight, height = @height where id = @id", connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("weight", weight);
                command.Parameters.AddWithValue("height", height);
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
                var command = new NpgsqlCommand("delete from fc.body_mass_index where id = @id", connection);
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
