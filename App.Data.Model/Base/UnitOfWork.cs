using App.Data.Model.Model;
using System;
using EFBulkInsert;
using App.Data.Model.Interface.Repository;
using System.Data.Entity;
using System.Linq;


namespace App.Data.Model.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private GloaithNationalEntities context = new GloaithNationalEntities();

        #region Repositories

        private Repository<Rate> rateRepository;

        public Repository<Rate> RateRepository
        {
            get
            {
                if (this.rateRepository == null)
                {
                    this.rateRepository = new Repository<Rate>(context);
                }
                return rateRepository;
            }
        }

        private Repository<ProductTransaction> transactionRepository;

        public Repository<ProductTransaction> TransactionRepository
        {
            get
            {
                if (this.transactionRepository == null)
                {
                    this.transactionRepository = new Repository<ProductTransaction>(context);
                }
                return transactionRepository;
            }
        }

        private Repository<Currency> currencyRepository;

        public Repository<Currency> CurrencyRepository
        {
            get
            {
                if (this.currencyRepository == null)
                {
                    this.currencyRepository = new Repository<Currency>(context);
                }
                return currencyRepository;
            }
        }

        #endregion

        public void Save()
        {
            context.SaveChanges();
        }

     
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RejectChanges()
        {
            foreach (var entry in context.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
