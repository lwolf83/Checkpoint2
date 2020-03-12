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
        public SqlConnection Connection { get; private set; }

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

            Connection = new SqlConnection(connectionString); ;
            Connection.Open();
        }


        public void Connect(SqlConnectionStringBuilder builder)
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                throw new Exception("Database already connected");
            }
            Connection.ConnectionString = builder.ConnectionString;
            Connection.Open();
        }

        public SqlDataReader ExecuteStoredProcedure(string name, List<SqlParameter> parameters)
        {
            SqlCommand cmd = new SqlCommand(name);
            cmd.Connection = Connection;
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