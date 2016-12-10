using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes
{
    class Booking
    {
        public User User
        {
            get;
            private set;
        }

        public String _UserName
        {
            get;
            private set;
        }
        public String _RoomName
        {
            get;
            private set;
        }
        public String StartDate
        {
            get;
            private set;
        }
        public String EndDate
        {
            get;
            private set;
        }
        public Booking(User user, String room,String dateS,String DateE)
        {
            this.User = user;
            this._UserName = user.Name;
            this._RoomName = room;
            this.StartDate = dateS;
            this.EndDate = DateE;
        }
    }
}
