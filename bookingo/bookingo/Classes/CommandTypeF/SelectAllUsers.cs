using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes.CommandTypeF
{
    class SelectAllUsers :CommandType
    {
        public List<User> users=new List<User>();
        private SqlCommand command = new SqlCommand();
        public SelectAllUsers(string commandTxt)
        {
            command.CommandText = commandTxt;
        }
        public void executeCommand(SqlConnection conn)
        {
            Users userFactory= new Users();
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                switch (reader[2].ToString())
                {
                    case "CEO":
                        users.Add(userFactory.createCEO(reader[0].ToString(), "CEO"));
                        break;
                    case "Manager":
                        users.Add(userFactory.createManager(reader[0].ToString(), "Manager"));
                        break;
                    case "Developer":
                        users.Add(userFactory.createDeveloper(reader[0].ToString(), "Developer"));
                        break;
                }
            }
        }
        public List<User> getUsers()
        {
            return users;
        }
    }
}
