using bookingo.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo
{
    abstract class User
    {
        public String Name
        {
            get;
            private set;
        }
        public String JobTitle
        {
            get;
            private set;
        }
        public List<Booking> Bookings
        {
            get;
            private set;
        }
        public User(String name,String jobTitle)
        {
            this.Name = name;
            this.JobTitle = jobTitle;
        }
        public void AddBooking(Booking booking)
        {
            this.Bookings.Add(booking);
        }
    }
}
