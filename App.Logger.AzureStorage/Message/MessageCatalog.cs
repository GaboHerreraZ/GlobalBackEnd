using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utility.Message
{

    /// <summary>
    /// Clase que obtiene el mensaje del archivo de recursos
    /// </summary>
    /// <returns></returns>
    /// <CreatedBy>Gabriel Herrera Z</CreatedBy>
    public class MessageCatalog
    {
        #region Variables
        private static volatile MessageCatalog instance;
        private static object syncRoot = new Object();
        private const string MSG_MENSAJE_NO_ENCONTRADO = "Mensaje no encontrado. {0}";

        public static MessageCatalog Instancia
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MessageCatalog();
                    }
                }

                return instance;
            }
        }
        #endregion

        /// <summary>
        /// Método que obtiene el mensaje del archivo de recursos
        /// </summary>
        /// <returns></returns>
        /// <CreatedBy>Gabriel Herrera Z</CreatedBy
        public string GetMessage(string code)
        {
            string descripcion;
            descripcion = Message.ResourceManager.GetString(code);

            if (string.IsNullOrEmpty(descripcion))
            {
                descripcion = string.Format(MSG_MENSAJE_NO_ENCONTRADO, code);
            }
            return descripcion;
        }
    }
}
