using App.Data.Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EFBulkInsert;
using App.Utility.Exceptions;
using App.Utility.Message;
using static App.Utility.Constants.Enum;
using App.Data.Model.Entity.Common;
using App.Data.Model.Interface.Repository;

namespace App.Data.Model.Base
{
    public class Repository<T>: IRepository<T> where T:class
    {
        internal GloaithNationalEntities context;
        internal DbSet<T> dbSet;

        public IQueryable<T> Entities => dbSet;


        public Repository(GloaithNationalEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual void BulkInsert(List<T> entity)
        {
            try
            {
                context.BulkInsert<T>(entity);
            }
            catch(Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }catch(Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);

            }

        }

        public virtual T GetByID(object id)
        {
            try
            {
                return dbSet.Find(id);
            }
            catch (Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }
        }

        public virtual void Insert(T entity)
        {
            try
            {
                dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }
        }

        public virtual void Delete(object id)
        {
            try
            {
                T entityToDelete = dbSet.Find(id);
                Delete(entityToDelete);
            }
            catch (Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }
        }
       
        public virtual void AllDelete()
        {
            try
            {
                context.Database.ExecuteSqlCommand($"TRUNCATE TABLE {typeof(T).Name}");
            }
            catch(Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }
        }
        public virtual void Delete(T entityToDelete)
        {
            try
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }
            catch (Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }

        }

        public virtual void Update(T entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }
        }

        public virtual int TotalRegisters()
        {
            return dbSet.Count();
        }

        public T[] ExecuteSentence<T>(string sentence)
        {
            try
            {
                return context.Database.SqlQuery<T>(sentence).ToArray();
            }
            catch (Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }
        }

        public T[] ExecuteProcedure<T>(SqlProcedure procedure)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendFormat("{0} {1}"
                    , procedure.ProcedureName
                    , string.Join(",", procedure.Parameters.Select(p => p.ParameterName + "=" + p.Value.ToString())));

                var selectedList = context
                             .Database
                             .SqlQuery<T>(query.ToString())
                             .ToArray<T>();

                return (T[])selectedList;
            }
            catch (Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }

        }

        public T[] ExecuteProcedure<T>(string procedureName)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendFormat("{0}"
                    , procedureName);

                var selectedList = context
                             .Database
                             .SqlQuery<T>(query.ToString())
                             .ToArray<T>();

                return (T[])selectedList;
            }
            catch (Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }

        }

        public int ExecuteProcedure(SqlProcedure procedure)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendFormat("{0} {1}"
                    , procedure.ProcedureName
                    , string.Join(",", procedure.Parameters.Select(p => p.ParameterName + "=" + p.Value.ToString())));

                var rowsAffected = context
                             .Database
                             .ExecuteSqlCommand(query.ToString());

                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new ExceptionOperation(HelperMessage.GetMessage(CodeMessage.GEN_001), (int)CodeHTTP.Error, ex.Message, ex.InnerException);
            }

        }

    }
}
