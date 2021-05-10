using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gozen.Data.Entity.Base;
using Microsoft.EntityFrameworkCore;

namespace Gozen.Data.Core
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            try
            {
                entity.IssueDate = DateTime.UtcNow;
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Attach(entity);
                var entry = _context.Entry(entity);
                entry.State = EntityState.Modified;
                if (entry.Property("IssueDate") != null) entry.Property("IssueDate").IsModified = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> SoftDeleteAsync(TEntity entity)
        {
            try
            {
                var dbEntity = await GetByIdAsync(entity.Id);
                dbEntity.IsActive = entity.IsActive;
                return await CreateAsync(dbEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageIndex = 0, int pageSize = 0)
        {
            return await _context.Set<TEntity>().Where(c => c.IsActive).OrderByDescending(c => c.Id)
                .Skip(pageIndex * pageSize).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate,
            int pageIndex = 0, int pageSize = 0)
        {
            return await _context.Set<TEntity>().Where(predicate).OrderByDescending(c => c.Id)
                .Skip(pageIndex * pageSize).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}