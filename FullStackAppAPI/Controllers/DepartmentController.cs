using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Data;
using System.Net.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using FullStackAppAPI.Models;

namespace FullStackAppAPI.Controllers
{
    public class DepartmentController : ApiController
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/market")]
        // GET: Department
        public HttpResponseMessage Get()
        {
            string query = @"Select * From ProductTbl";

            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["smarketdb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                adapter.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [System.Web.Http.HttpPost]
        public string Post(Department dep)
        {
            try
            {
                string query = @"Insert Into Department Values('" + dep.DepartmentName + @"')";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    adapter.Fill(table);
                }
                return "Added Successfully";

            }
            catch
            {
                return "Failed to Add!";
            }
        }

        [System.Web.Http.HttpPut]

        public string Put(Department dep)
        {
            try
            {
                string query = @"Update Department set DepartmentName='" + dep.DepartmentName + @"' where DepartmentId=" + dep.DepartmentId + @"";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    adapter.Fill(table);
                }
                return "Updated Successfully";

            }
            catch
            {
                return "Failed to Updated!";
            }
        }

        [System.Web.Http.HttpDelete]

        public string Delete(int id)
        {
            try
            {

                string query = @"delete from Department where DepartmentId=" + id + @"";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    adapter.Fill(table);
                }
                return "Deleted Successfully";

            }
            catch
            {
                return "Failed to Delete!";
            }
        }

    }
}