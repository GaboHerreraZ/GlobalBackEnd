using App.Data.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Model.Interface.Repository
{
    interface IRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        void BulkInsert(List<T> Entity);

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T GetByID(object id);
        void Insert(T entity);

        void Delete(object id);

        void AllDelete();

        void Delete(T entityToDelete);

        void Update(T entityToUpdate);

        int TotalRegisters();

        T[] ExecuteSentence<T>(string sentence);

        T[] ExecuteProcedure<T>(SqlProcedure procedure);

        T[] ExecuteProcedure<T>(string procedureName);

        int ExecuteProcedure(SqlProcedure procedure);
    }
}
