using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ManyManyEFCore.Repository.Pattern
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        Task<bool> Rollback();
    }
}
