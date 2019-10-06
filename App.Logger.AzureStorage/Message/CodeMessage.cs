using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utility.Message
{

    /// <summary>
    /// Clase creada para el manejo de códigos de mensajes en el 
    /// front-end y excepciones en log de Azure
    /// </summary>
    /// <returns></returns>
    /// <CreatedBy>Gabriel Herrera Z</CreatedBy>
    public class CodeMessage
    {
        #region Errores generales
        public const string GEN_001 = "GEN_001";
        #endregion


        #region Errores integracion
        public const string INT_001 = "INT_001";
        #endregion

        #region Errores Transaction
        public const string TRN_001 = "TRN_001";
        #endregion

        #region Errores Rate
        public const string RAT_001 = "RAT_001";
        #endregion

        
    }
}
