using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Test.Controllers
{
    public class HelloController : ApiController
    {

        [System.Web.Http.HttpGet]
        [Route("api/hello")]
        public IEnumerable<string> Values()
        {
            string query = @"Select DepartmentId, DepartmentName From [Employees].[dbo].[Department]";

            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["Employee"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                adapter.Fill(table);
            }

            return (IEnumerable<string>)Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [System.Web.Http.HttpGet]
        public string Value(int id)
        {
            return "value";
        }

        [System.Web.Http.HttpPost]
        public void SaveNewValue([FromBody] string value)
        {
        }

        [HttpPut]
        public void UpdateValue(int id, [FromBody] string value)
        {
        }

        [HttpDelete]
        public void RemoveValue(int id)
        {
        }

    }
}



