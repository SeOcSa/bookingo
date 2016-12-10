using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bookingo.Classes.CommandTypeF
{
    class SelectRooms : CommandType
    {
        private SqlCommand command = new SqlCommand();
        private List<Room> roomList = new List<Room>();
        public SelectRooms(String _command)
        {
            command.CommandText = _command;
        }
        public void executeCommand(SqlConnection conn)
        {
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {  
                roomList.Add(new Room(
                    reader[0].ToString(),
                    reader[1].ToString(),
                    reader[2].ToString(),
                    reader[3].ToString(),
                    "/imgSource/Meeting_Room.png"
                    ));
            }
        }
        public List<Room> allRooms()
        {
            return this.roomList;
        }
    }
}
