using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Model.Entity.Common
{
    /// <summary>
    /// Clase que maneja la comunicación con la capa de presentación
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    /// <CreatedBy>Gabriel Herrera Z</CreatedBy>
    /// <CreationDate>278/11/2018</CreationDate>
    public class Response
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public Object Data { get; set; }
    }
}
