using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace Database
{
    public class Connect
    {
        SqlConnection con = new SqlConnection();
        public DataTable table = new DataTable();
        private static readonly ThreadLocal<int> instancesLocal = new System.Threading.ThreadLocal<int>();
        private static Dictionary<int, Connect> instances = new Dictionary<int, Connect>();
        public Connect()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["smarketdb"].ConnectionString;
        }

        public Connect Instance
        {
            get
            {
                int currentID = instancesLocal.Value;
                if (Monitor.TryEnter(Instance))
                {
                    if (!instances.ContainsKey(currentID))
                    {
                        return instances[currentID] = new Connect();
                    }
                    else
                    {
                        return instances[currentID];
                    }
                }
                else
                {
                    return instances[currentID] = new Connect();
                }
            }
        }



        public void retrieveData(string command)
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