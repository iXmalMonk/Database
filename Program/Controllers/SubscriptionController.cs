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
            List<SubscriptionModel> subscriptionList = new List<SubscriptionModel>();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("select * from fc.subscription", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var subscription = new SubscriptionModel
                    {
                        Id = reader.GetInt32(1),
                        Visit = reader.GetInt32(2),
                        TypeOfOccupationId = reader.GetInt32(3)
                    };
                    subscriptionList.Add(subscription);
                }
                connection.Close();
            }
            return View(subscriptionList);
        }

        public IActionResult Insert(int clientId, int groupId, int visit)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json").Build();
            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new NpgsqlCommand("call fc.client_group_subscription_procedure_insert(@client_id, @group_id, @visit", connection);
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
