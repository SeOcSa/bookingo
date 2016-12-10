using bookingo.Classes.CommandTypeF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes
{
    class DatabaseConnection
    {
        private SqlConnection sqlConnection;
        private static List<User> allUser;

        Boolean status;
        public String Connection
        {
            get;
            private set;
        }

        public String Command
        {
            get;
            private set;
        }
        public void setAllUser(List<User> users)
        {
            allUser = users;
        }
        public DatabaseConnection(String _connection,String _command)
        {
            this.Connection = _connection;
            this.Command = _command;
            this.openConnection();
        }
        public User getLoginUser()
        {
            CommandType loginUser = new SelectLoginUser(Command);
            loginUser.executeCommand(this.sqlConnection);
            status = ((SelectLoginUser)loginUser).getStatus();
            this.sqlConnection.Close();
            return ((SelectLoginUser)loginUser).getUser();
        }
        public void getUserName()
        {
            CommandType UserName = new SelectUser(Command);
            UserName.executeCommand(this.sqlConnection);
            status = ((SelectUser)UserName).getStatus();
            this.sqlConnection.Close();
        }
        public void queryCommandType()
        {
            CommandType query = new ExecutQueryCommandType(Command);
            query.executeCommand(this.sqlConnection);
            this.sqlConnection.Close();
        }
        public List<Booking> getBooking()
        {
            CommandType query = new SelectBookings(Command, allUser);
            query.executeCommand(this.sqlConnection);
            this.sqlConnection.Close();
            return ((SelectBookings)query).getBookings();
        }
        public SelectRooms getRooms()
        {
            CommandType query = new SelectRooms(Command);
            query.executeCommand(this.sqlConnection);
            this.sqlConnection.Close();
            return (SelectRooms)query;
        }
        private void openConnection()
        {
            sqlConnection = new SqlConnection(this.Connection);
            sqlConnection.Open();
        }
        public Boolean getStatus()
        {
            return status;
        }

        public List<User> getAllUser()
        {
            CommandType loginUser = new SelectAllUsers (Command);
            loginUser.executeCommand(this.sqlConnection);
            this.sqlConnection.Close();
            return ((SelectAllUsers)loginUser).getUsers();
        }
    }
}
