﻿using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess
{
    public class Connect
    {
        SqlConnection con = new SqlConnection();
        public DataTable table = new DataTable();
        
        public Connect()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["smarketdb"].ConnectionString;
        }

        public void retrieve_data(string command)
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command, con);
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                con.Close();
            }
        }

        public void commandExc(string command)
        {
            try
            {
                con.Open();
                SqlCommand sqlcomm = new SqlCommand(command, con);

                int rowInfected = sqlcomm.ExecuteNonQuery();
                if (rowInfected > 0)
                {
                    Console.WriteLine("Success to connect with db!");
                }
                else
                {
                    Console.WriteLine("Fail to connect with db!");
                }
            }
            catch (Exception ex)
            {

            }
        }


    }
}