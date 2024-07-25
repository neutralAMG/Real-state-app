

using FinalProject.Core.Application.Core;
using FinalProject.Infraestructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalProject.Infraestructure.Persistance.Core
{
    public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
        where TEntity : class
    {
        private readonly FinalProjectContext _context;
        private readonly DbSet<TEntity> _entity;

        public BaseRepository(FinalProjectContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entity.AnyAsync(predicate);
        }

        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            try
            {
                await _entity.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _entity.Attach(entity);
                _entity.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                _entity.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class BaseCompleteRepository<TEntity, TId> : BaseRepository<TEntity, TId>, IBaseCompleteRepository<TEntity, TId>
        where TEntity : class
    {
        private readonly FinalProjectContext _context;
        private readonly DbSet<TEntity> _entity;
        public BaseCompleteRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _entity.FindAsync(id);
        }
    }
}
