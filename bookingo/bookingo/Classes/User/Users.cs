using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes
{
     class Users
    {
        public CEO createCEO(String name,String jobTitle)
        {
            return new CEO(name, jobTitle);
        }

        public Developer createDeveloper(String name, String jobTitle)
        {
            return new Developer(name, jobTitle);
        }

        public Manager createManager(String name, String jobTitle)
        {
            return new Manager(name, jobTitle);
        }
    }
}
