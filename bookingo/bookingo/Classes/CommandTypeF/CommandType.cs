﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookingo.Classes.CommandTypeF
{
    interface CommandType
    {
        void executeCommand(SqlConnection conn);
    }
}