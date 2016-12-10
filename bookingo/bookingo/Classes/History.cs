using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes
{
    class History
    {
        private StringBuilder content = new StringBuilder();

        public void add(String _content)
        {
            content.Append(_content + "\n");
        }
    }
}
