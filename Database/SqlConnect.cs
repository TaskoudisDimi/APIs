using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Sockets;

namespace Database
{
    public class SqlConnect
    {
        private static SqlConnect instance = null;
        private static readonly object padlock = new object();
        public static int queryTimeOut = 20;
        static Dictionary<int, SqlConnect> instances = new Dictionary<int, SqlConnect>();
        private static int instanceID;
        private static readonly ThreadLocal<int> currentInstanceId = new ThreadLocal<int>();
        public static string connectionString;
        public SqlDataReader reader = null;
        public SqlConnection connection = null;

       

        public static SqlConnect Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SqlConnect();
                    }
                    return instance;
                }
            }
        }

        public bool CheckConnection()
        {
            try
            {
                CloseConnection();
            }
            catch (Exception ex)
            {
                
            }
            if (connection == null || connection.State != ConnectionState.Open)
            {
                return openConnection();
            }
            return true;
        }

        private void CloseConnection(bool OK = true)
        {
            try
            {
                if (connection == null)
                    return;
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        public bool openConnection()
        {
            CloseConnection();
            connection = new SqlConnection(connectionString);
            try
            {
                using (Locker.Lock(instances))
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                
                connection.Dispose();
                connection = null;
                instances.Remove(currentInstanceId.Value);
                SqlConnection.ClearAllPools();
            }
            return connection != null;
        }

        public SqlDataReader SelectDataReader(string sql, List<SqlParameter> parameters = null)
        {

            if (!CheckConnection())
            {
                return null;
            }
            using (Locker.Lock(instances))
            {
                DateTime dtStart = DateTime.Now;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = queryTimeOut;
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }
                try
                {
                    reader = cmd.ExecuteReader();
                    instances.Remove(currentInstanceId.Value);
                }
                catch
                {

                }
                long dt = (long)((DateTime.Now - dtStart).TotalMilliseconds);
                if (dt > 100)
                {
                    
                }
            }
            return reader;
        }

        public int Insert(string cmd)
        {
            if (!CheckConnection())
            {
                return -1;
            }
            try
            {
                SqlCommand command = new SqlCommand(cmd, connection);
                return command.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }

        }

        public int Update(string cmd)
        {
            if (!CheckConnection())
            {
                return -1;
            }
            try
            {
                SqlCommand command = new SqlCommand(cmd, connection);
                return command.ExecuteNonQuery();

            }
            catch
            {
                return -1;
            }
        }

        public int Delete(string cmd)
        {
            if (!CheckConnection())
            {
                return -1;
            }
            try
            {
                SqlCommand command = new SqlCommand(cmd, connection);
                return command.ExecuteNonQuery();

            }
            catch
            {
                return -1;
            }
        }

        public DataTable SelectDataTable(string sql, List<SqlParameter> parameters = null)
        {
            DataTable dt = new DataTable();
            DateTime dtStart = DateTime.Now;
            if (!CheckConnection())
            {
                return null;
            }
            using (Locker.Lock(instances))
            {
                try
                {
                    reader = SelectDataReader(sql, parameters);
                    dt.Load(reader);
                }
                catch
                {

                }
                long time = (long)((DateTime.Now - dtStart).TotalMilliseconds);
                if (time > 100)
                {
                    
                }
            }
            return dt;
        }

        public DataSet ExecDataSet()
        {
            DataSet set = new DataSet();

            return set;
        }





    }

    
}
