using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Data;

namespace FullStackAppAPI.Controllers
{
    public class DepartmentController : ApiController
    {
        // GET: Department
        public HttpResponseMessage Get()
        {
            string query = @"Select DepartmentId, DepartmentName From dbo.Department";

            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionString["Employee"].ConnectionString))

                using (var cmd = new SqlCommand(query, con)) ;
            using (var adapter = new SqlAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                adapter.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK.table);
        }


    }
}