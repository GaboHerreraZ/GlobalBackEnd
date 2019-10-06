using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utility.Constants
{
    public class Enum
    {
        [Flags]
        public enum CodeHTTP : int
        {
            Ok = 200,
            No_Autorizado = 401,
            NoContent = 204,
            Error = 400,
            Error_Servicio = 500,

        }
    }
}
