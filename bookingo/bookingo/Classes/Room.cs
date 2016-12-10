using bookingo.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bookingo
{
    class Room
    {
        public String Name
        {
            get;
            private set;
        }
        public String MaxCapacity
        {
            get;
            private set;
        }
        public String Floor
        {
            get;
            private set;
        }
        public String Asserts
        {
            get;
            private set;
        }
        public String Image
        {
            get;
            private set;
        }
        public List<Booking> Bookings
        {
            get;
            private set;
        }
        public Room(String name, String maxC, String _floor, String _asserts, String path)
        {
            this.Name = "Room Name: " + name;
            this.MaxCapacity = "Capacity:" + maxC;
            this.Floor = "Floor: " + _floor;
            this.Asserts = "Asserts: " + _asserts;
            this.Image = path;
        }
    }
}
