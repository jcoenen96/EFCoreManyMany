using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Repository.Pattern
{
    public interface IRepository<TModelEntity, TDatabaseEntity> 
        where TModelEntity : class
        where TDatabaseEntity : class
    {
        Task<TModelEntity> FindAsync(int id);
        Task<List<TModelEntity>> GetAll();
        Task AddAsync(TModelEntity entity);
        Task AddRangeAsync(IEnumerable<TModelEntity> entities);
        void Update(TModelEntity entity);
        Task RemoveAsync(int id);
        void Remove(TModelEntity entity);
    }
}
