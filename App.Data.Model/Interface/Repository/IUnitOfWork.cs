using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Model.Interface.Repository
{
    interface IUnitOfWork
    {

        void Save();
        void Dispose();
        void RejectChanges();
    }
}
