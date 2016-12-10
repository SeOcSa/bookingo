using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bookingo.Classes
{
    class DataValidation
    {
        public String SqlConn
        {
            get;
            private set;
        }
        public String SqlComm
        {
            get;
            private set;
        }
        public DataValidation(String _conn,String _comm)
        {
            this.SqlConn = _conn;
            this.SqlComm = _comm;
        }
        public void testUserName(String userName)
        {
            if(userName.Length<3)
            {
                throw new Exception("User name length is to short.");
            }

            DatabaseConnection dbConnection = new DatabaseConnection(SqlConn, SqlComm);
            dbConnection.getUserName();
            
            if(dbConnection.getStatus())
            {
                throw new Exception("User name is taked!");
            }
        }
        public void testPassword(String password)
        {
            if(String.IsNullOrEmpty(password))
            {
                throw new Exception("Password has no value inserted.");
            }

            try
            {
                if (!Regex.IsMatch(password, @"(?=^[^\s]{6,}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[@#$%^&+=])"))
                {
                    throw new Exception("Password is invalid.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void confirmPassword(String passWord,String confirmPassWord)
        {
            if(!passWord.Equals(confirmPassWord))
            {
                throw new Exception("Passwords do not match.");
            }
        }
        public void testJobTitle(String jobTitle)
        {
            if(String.IsNullOrEmpty(jobTitle) || "-1".Equals(jobTitle))
            {
                throw new Exception("Select your job title.");
            }
        }
        public void selectedDateTime(DateTime start, DateTime stop)
        {
            if(start == stop)
            {
                throw new Exception("Same date time selected.\nPay attention!");
            }
            if(start >stop)
            {
                throw new Exception("Invalid selection: " + start.ToString() + " > " + stop.ToString());
            }

            if(start.Day != stop.Day)
            {
                throw new Exception("Not the same day.");
            }
        }
        public void testFreePosition(DateTime start, DateTime stop)
        {
            DateTime getDateTimeStart, getDateTimeStop;
            DatabaseConnection dbConnection = new DatabaseConnection(SqlConn, SqlComm);
            
            var bookings = dbConnection.getBooking();

            foreach( var booking in bookings)
            {
                getDateTimeStart = Convert.ToDateTime(booking.StartDate);
                getDateTimeStop = Convert.ToDateTime(booking.EndDate);

                if (start == getDateTimeStart || (stop >= getDateTimeStart && stop <= getDateTimeStop) ||
                    (start >= getDateTimeStart && start <= getDateTimeStop))
                    throw new Exception("Room is booked already.");
            }
        }
    }
}
