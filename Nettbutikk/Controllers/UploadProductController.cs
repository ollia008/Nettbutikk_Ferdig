using Microsoft.AspNetCore.Mvc;
using Nettbutikk.Models;
using System.Data;
using System.Data.SqlClient;

namespace Nettbutikk.Controllers
{
    public class UploadProductController : Controller
    {
        [HttpGet]
        public IActionResult UploadProduct()
        {

            return View();

        }

        [HttpPost]
        public IActionResult UploadProduct(UploadProductModel objUploadProductModel)
        {
            string conStr = "Data Source=.\\SQLEXPRESS; Initial Catalog=nettbutikk; Integrated Security=True";
            SqlConnection con = new SqlConnection(conStr);
            string qry = $"insert into Products values (@ImageData, @Pris)";

            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1);
            cmd.Parameters.AddWithValue("@Pris", objUploadProductModel.pris);

            //https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iformfile?view=aspnetcore-9.0

            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                objUploadProductModel.picture.CopyTo(memoryStream);
                imageData = memoryStream.ToArray();
            }

            cmd.Parameters["@ImageData"].Value = imageData;

            cmd.ExecuteNonQuery();

            con.Close();

            return View();


        }

    }
}
