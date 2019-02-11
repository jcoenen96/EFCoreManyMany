using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ManyManyEFCore.Repository.Pattern
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDbContextTransaction Transaction { get; private set; }
        private readonly DbContext dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if(dbContext.Database.CurrentTransaction == null)
            {
                Transaction = dbContext.Database.BeginTransaction();
            }
            else
            {
                Transaction = dbContext.Database.CurrentTransaction;
            }
        }

        public virtual bool Commit()
        {
            if(Transaction == null)
            {
                return false;
            }

            Transaction.Commit();

            return true;
        }

        public virtual async Task<bool> Rollback()
        {
            if(Transaction == null)
            {
                return false;
            }

            Transaction.Rollback();

            foreach (EntityEntry entry in dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        await entry.ReloadAsync();
                        break;
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        
    }
}
