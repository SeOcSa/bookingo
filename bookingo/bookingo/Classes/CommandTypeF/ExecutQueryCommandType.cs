using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes.CommandTypeF
{
    class ExecutQueryCommandType:CommandType
    {
        SqlCommand command= new SqlCommand();

        public ExecutQueryCommandType(String _commandTxt)
        {
            command.CommandText = _commandTxt;
        }

        public void executeCommand(SqlConnection conn)
        {
            command.Connection = conn;

            command.ExecuteNonQuery();
        }
    }
}
