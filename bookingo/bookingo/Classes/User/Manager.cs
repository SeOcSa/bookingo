using bookingo.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo
{
    class Manager : User, UserAction
    {
        public Manager(string name, string jobTitle) : base(name, jobTitle)
        {
        }

        public void create()
        {
            throw new NotImplementedException();
        }

        public void remove()
        {
            throw new NotImplementedException();
        }

        public void update()
        {
            throw new NotImplementedException();
        }
    }
}
