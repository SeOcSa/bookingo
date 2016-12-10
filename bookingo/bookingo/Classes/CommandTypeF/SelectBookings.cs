using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes.CommandTypeF
{
    class SelectBookings : CommandType
    {
        private SqlCommand command = new SqlCommand();

        public List<User> allUsers
        {
            get;
            private set;
        }

        List<Booking> bookings = new List<Booking>();
        public SelectBookings( String _command, List<User> users)
        {
            this.command.CommandText = _command;
            this.allUsers = users;
        }
        public void executeCommand(SqlConnection conn)
        {
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User findElement = getItem(reader[0].ToString());

                if(findElement != null)
                    bookings.Add(new Booking(findElement, reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
            }
        }
        private User getItem(String userName)
        {
            foreach (var userItem in allUsers)
                if (userItem.Name == userName)
                    return userItem;
            return null;
        }
        public List<Booking> getBookings()
        {
            return this.bookings;
        }
    }
}
