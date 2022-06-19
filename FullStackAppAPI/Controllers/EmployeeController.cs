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
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace FullStackAppAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"Select EmployeeId, EmployeeName, Department 
                            Convert(varvhar(10), DateOfJoining, 120) as DateOfJoining,
                            PhotoFilename
                            From [Employees].[dbo].[Employee]";

            DataTable table = new DataTable();

            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["Employee"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                adapter.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        public string Post(Employee emp)
        {
            try
            {
                string query = @"Insert Into Employee Values
(                               (
                                '" + emp.EmployeeName + @"'
                                ,'" + emp.Department + @"'
                                ,'" + emp.DateOfJoining + @"'
                                ,'" + emp.PhotoFileName + @"'
                                )";
                               
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


        public string Put(Employee emp)
        {
            try
            {
                string query = @"Update Employee set 
                                EmployeeName='" + emp.EmployeeName + @"'
                                DepartmentName = '" + emp.Department + @"'
                                DateOfJoining = '" + emp.DateOfJoining + @"'
                                PhotoFileName = '" + emp.PhotoFileName + @"'
                                where EmployeeId=" + emp.EmployeeId + @"";
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


        public string Delete(int id)
        {
            try
            {

                string query = @"delete from Employee where EmployeeId=" + id + @"";
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


        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]
        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"Select DepartmentName From Department";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
            ConnectionStrings["Employee"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                adapter.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
    


        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var path = HttpContext.Current.Server.MapPath("/Photos/" + fileName);

                postedFile.SaveAs(path);

                return fileName;


            }
            catch (Exception ex)
            {
                return "anonymous.png";
            }
        }
    
    }


}