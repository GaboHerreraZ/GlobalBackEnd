using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Model.Entity.Common
{
    public class SqlProcedure
    {
        public string ProcedureName { get; set; }
        public SqlParameter[] Parameters { get; set; }

        public SqlProcedure(string procedureName)
        {
            ProcedureName = procedureName;
            Parameters = new SqlParameter[0];
        }

        public SqlProcedure(string procedureName, params SqlParameter[] parameters)
        {
            ProcedureName = procedureName;
            Parameters = parameters;
        }
    }
}
