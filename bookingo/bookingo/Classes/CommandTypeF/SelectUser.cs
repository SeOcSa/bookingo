using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes.CommandTypeF
{
    class SelectUser:CommandType
    {
        public Boolean status;
        private SqlCommand command = new SqlCommand();
        public SelectUser(string commandTxt)
        {
            command.CommandText = commandTxt;
        }
        public void executeCommand(SqlConnection conn)
        {
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read() == true)
            {
                status = true;
            }
            else
            {
                status = false;
            }
        }
        public Boolean getStatus()
        {
            return status;
        }
        
    }
}
