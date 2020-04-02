using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> Remove(T entity);
        Task<bool> Remove(object id);
        Task<bool> Update(T entity);
        // Task<int> SaveChangesAsync();

    }
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IUnitOfWork UW;
        protected readonly DbSet<T> _repo;
        public BaseRepository(IUnitOfWork UW)
        {
            this.UW = UW;
            this._repo = UW.DB.Set<T>();
        }
        public async virtual Task<bool> AddAsync(T entity)
        {
            try
            {
                var result = await _repo.AddAsync(entity);
                if (result.State == EntityState.Added)
                {
                    await UW.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public async virtual Task<bool> Remove(T entity)
        {
            try
            {
                var result = _repo.Remove(entity);
                if (result.State == EntityState.Deleted)
                {
                    await UW.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public async virtual Task<bool> Remove(object id)
        {
            try
            {
                var FindResult = _repo.Find(id);
                if (FindResult != null)
                {
                    var result = _repo.Remove(FindResult);
                    if (result.State == EntityState.Deleted)
                    {
                        await UW.Save();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        //public async Task<int> SaveChangesAsync()
        //{
        //    var result=await _ctx.SaveChangesAsync();
        //    return result;
        //}

        public async virtual Task<bool> Update(T entity)
        {
            try
            {
                var result = _repo.Update(entity);
                if (result.State == EntityState.Modified)
                {
                    await UW.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }


    }
}
