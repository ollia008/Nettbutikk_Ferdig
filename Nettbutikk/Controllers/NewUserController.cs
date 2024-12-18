using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Nettbutikk.Controllers
{
    public class NewUserController : Controller
    {
        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewUser(LoginnModel objLoginnModel)
        {

            string conStr = "Data Source=.\\SQLEXPRESS; Initial Catalog=nettbutikk; Integrated Security=True";
            SqlConnection con = new SqlConnection(conStr);
            string bruker = objLoginnModel.Username;
            string passord = objLoginnModel.Password;
            string qry = $"insert into LoginInfo values ('{bruker}', '{passord}')";
            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();

            return View();
        }
    }
}
