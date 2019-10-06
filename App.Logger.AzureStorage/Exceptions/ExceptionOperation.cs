using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utility.Exceptions
{

    /// <summary>
    /// Clase creada para el control de excepciones manejadas
    /// </summary>
    /// <param name="message"></param>
    /// <param name="codigo"></param>
    /// <param name="data"></param>
    /// <param name="innerException"></param>
    /// <returns></returns>
    /// <CreatedBy>Gabriel Herrera Z</CreatedBy>
    public class ExceptionOperation:ExceptionBase
    {
        #region Variables
        private string _operacion;
        private int _codigo;
        public string Operation
        {
            get { return _operacion; }
            set { _operacion = value; }
        }
        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        #endregion

        /// <summary>
        /// Constructor para excepciones de operacion. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="codigo"></param>
        /// <param name="data"></param>
        /// <param name="innerException"></param>
        /// <returns></returns>
        /// <CreatedBy>Gabriel Herrera Z</CreatedBy>
        public ExceptionOperation(string message, int codigo, object data, Exception innerException = null)
            : base(message, data, innerException)
        {
            this._codigo = codigo;
            this._operacion = GetCallerMethod();
        }
        /// <summary>
        /// Metodo que realiza los llamados a las excepciones. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>string</returns>
        /// <CreatedBy>Gabriel Herrera Z</CreatedBy>
        /// <CreationDate>10/10/2018</CreationDate>
        public string GetCallerMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(2);
            return sf.GetMethod().Name;
        }

    }
}
