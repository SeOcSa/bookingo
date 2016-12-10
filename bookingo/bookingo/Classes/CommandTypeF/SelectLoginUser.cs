using bookingo.Classes.CommandTypeF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes
{
    class SelectLoginUser : CommandType
    {
        private User loginUser;
        private Boolean status=false;
        private SqlCommand command=new SqlCommand();
        public SelectLoginUser(string commandTxt)
        {
            command.CommandText = commandTxt;
        }
        public void executeCommand(SqlConnection connection)
        {
            Users userFactory = new Users();
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read() == true)
            {
                status = true;
                switch (reader[1].ToString())
                {
                    case "CEO":
                        loginUser = userFactory.createCEO(reader[0].ToString(), reader[1].ToString());
                        break;
                    case "Manager":
                        loginUser = userFactory.createManager(reader[0].ToString(), reader[1].ToString());
                        break;
                    case "Developer":
                        loginUser = userFactory.createDeveloper(reader[0].ToString(), reader[1].ToString());
                        break;
                }
            }
        }
        public User getUser()
        {
            return loginUser;
        }
        public Boolean getStatus()
        {
            return status;
        }
    }
}
