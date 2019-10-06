using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utility.Message
{
    /// <summary>
    /// Clase que ayuda con el armado del mensaje
    /// </summary>
    /// <returns></returns>
    /// <CreatedBy>Gabriel Herrera Z</CreatedBy>
    public class HelperMessage
    {
        /// <summary>
        /// Método que setea el mensaje obtenido
        /// </summary>
        /// <returns></returns>
        /// <CreatedBy>Gabriel Herrera Z</CreatedBy>
        public static string GetMessage(string code)
        {
            try
            {
                return string.Format("{0}-{1}", code, MessageCatalog.Instancia.GetMessage(code));
            }
            catch (Exception ex)
            {
                //Escribir logger
                return string.Format("Error al obtener el mensaje del archivo,Razón:{0}", ex.Message);
            }
        }
    }
}
