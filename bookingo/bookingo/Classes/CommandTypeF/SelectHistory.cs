using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes
{
    class SelectHistory 
    {
        private SqlCommand command;
        List<History> history = new List<History>();

        public SelectHistory(String _commandTxt)
        {
            command.CommandText = _commandTxt;
            
        }

        public void executeCommand()
        {
            //add command. specific method
        }

        public List<History> getHistory()
        {
            return history;
        }
    }
}
