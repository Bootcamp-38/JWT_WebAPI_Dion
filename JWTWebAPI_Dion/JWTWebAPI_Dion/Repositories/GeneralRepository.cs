using JWTWebAPI_Dion.Base;
using JWTWebAPI_Dion.Context;
using JWTWebAPI_Dion.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebAPI_Dion.Repositories
{
    public class GeneralRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : MyContext
    {
        private readonly MyContext myContext;
        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public async Task<TEntity> Delete(int id)
        {
            var entity = await Get(id);
            if (entity != null)
            {
                myContext.Set<TEntity>().Remove(entity);
                await myContext.SaveChangesAsync();
                return entity;
            }
            return entity;
        }

        public async Task<List<TEntity>> Get()
        {
            return await myContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Get(int id)
        {
            return await myContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Post(TEntity entity)
        {
            await myContext.Set<TEntity>().AddAsync(entity);
            await myContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Put(TEntity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            await myContext.SaveChangesAsync();
            return entity;
        }
    }
}
