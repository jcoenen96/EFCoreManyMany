using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyManyEFCore.Repository.Pattern
{
    public abstract class Repository<TModelEntity, TDatabaseEntity> : IRepository<TModelEntity, TDatabaseEntity>
        where TModelEntity : class
        where TDatabaseEntity : class
    {
        protected DbContext DbContext { get; }
        protected DbSet<TDatabaseEntity> DbSet => DbContext.Set<TDatabaseEntity>();
        protected DbSet<T> GetDbSet<T>() where T : class
        {
            return DbContext.Set<T>();
        }

        protected IUnitOfWork UnitOfWork { get; }

        protected IMapper AutoMapper { get; }

        public Repository(DbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            DbContext = dbContext;
            UnitOfWork = unitOfWork;
            AutoMapper = mapper;
        }

        public async Task<TModelEntity> FindAsync(int id)
        {
            return AutoMapper.Map<TDatabaseEntity, TModelEntity>(await FindDbEntityAsync(id));
        }

        private async Task<TDatabaseEntity> FindDbEntityAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<TModelEntity>> GetAll()
        {
            return AutoMapper.Map<List<TDatabaseEntity>, List<TModelEntity>>(await GetDbSet<TDatabaseEntity>().ToListAsync());
        }

        public virtual async Task AddAsync(TModelEntity entity)
        {
            await DbSet.AddAsync(AutoMapper.Map<TModelEntity, TDatabaseEntity>(entity));
        }

        public virtual async Task AddRangeAsync(IEnumerable<TModelEntity> entities)
        {
            await DbSet.AddRangeAsync(AutoMapper.Map<IEnumerable<TModelEntity>, IEnumerable<TDatabaseEntity>>(entities));
        }

        public virtual void Update(TModelEntity entity)
        {
            var dbEntity = AutoMapper.Map<TModelEntity, TDatabaseEntity>(entity);
            DbSet.Attach(dbEntity);
            DbSet.Update(dbEntity);
        }

        public virtual async Task RemoveAsync(int id)
        {
            TDatabaseEntity entity = await FindDbEntityAsync(id);
            if (entity == null) { throw new KeyNotFoundException($"Id: {id} not found"); }
            RemoveDbEntity(entity);
        }

        public virtual void Remove(TModelEntity entity)
        {
            var dbEntity = AutoMapper.Map<TModelEntity, TDatabaseEntity>(entity);
            RemoveDbEntity(dbEntity);
        }

        private void RemoveDbEntity(TDatabaseEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached) { DbSet.Attach(entity); }
            DbSet.Remove(entity);
        }
    }
}
