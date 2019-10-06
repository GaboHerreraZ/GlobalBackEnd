using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exceptions
{   
     /// <summary>
     /// Clase base que hereda el exception
     /// </summary>
     /// <param name=""></param>
     /// <returns></returns>
     /// <CreatedBy>Gabriel Herrera</CreatedBy>
     /// <CreationDate>28/11/2018</CreationDate>
    public class ExceptionBase : Exception
    {
       
        private readonly object _referencia;
        public object Referencia { get { return _referencia; } }
        protected ExceptionBase(string message, object referencia, Exception innerException = null)
            : base(message, innerException)
        {
            _referencia = referencia;
        }
    }
}
