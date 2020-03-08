using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Data;

namespace WCS
{
    public class Database
    {
        private static Database _instance = null;
        private SqlConnection _connection = null;

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        private Database()
        {

            string connectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=WCS_CHECKPOINT2;Integrated Security=True;MultipleActiveResultSets=true";
            SqlConnection conn = new SqlConnection(connectionString);

            _connection = conn;

            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }


        public void Connect(SqlConnectionStringBuilder builder)
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                throw new Exception("Database already connected");
            }
            _connection.ConnectionString = builder.ConnectionString;
            _connection.Open();
        }

        public SqlConnection GetConnection()
        {
            return Database.Instance._connection;
        }

        public static SqlDataReader ExecuteStoredProcedure(string name, List<SqlParameter> parameters)
        {
            SqlCommand cmd = new SqlCommand(name);
            cmd.Connection = Database.Instance.GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;
            if(parameters.Count > 0)
            {
                foreach(SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Dispose();
            return reader;
        }
    }
}