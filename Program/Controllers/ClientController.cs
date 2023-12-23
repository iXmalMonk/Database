using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Program.Models;

namespace Program.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
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
            return View(clientList);
        }

        public IActionResult Insert(string fullName, decimal weight, decimal height)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("call fc.body_mass_index_client_procedure_insert(@full_name, @weight, @height)", connection);
                command.Parameters.AddWithValue("full_name", fullName);
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
                var command = new NpgsqlCommand("delete from fc.client", connection);
                command.Parameters.AddWithValue("id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
