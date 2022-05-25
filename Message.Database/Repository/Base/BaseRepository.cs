using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Message.Common.Base;
using Message.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Message.Database.Repository.Base
{
    public abstract class BaseRepository<TModel>
        where TModel : BaseModel
    {
        private ApplicationContext _context;

        protected BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<TModel>> GetByFilter(Expression<Func<TModel, bool>> predicate)
        {
            var result = await GetAll().AsNoTracking().Where(predicate).AsQueryable().ToListAsync();
            return result;
        }
        
        private IQueryable<TModel> GetAll()
        {
            return _context.Set<TModel>().AsQueryable();
        }
        
        public async Task<TModel> Create(TModel item)
        {
            item.Created = DateTime.Now;
            await _context.Set<TModel>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        public async Task<TModel> Update(TModel item)
        {
            item.Updated = DateTime.Now;
            _context.Set<TModel>().Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public TModel GetOne (Func<TModel, bool> predicate)
            => _context.Set<TModel>().AsNoTracking().FirstOrDefault(predicate);
        
        public async Task<TModel> GetById(Guid id)
            => await _context.Set<TModel>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<TModel> Delete(TModel item)
        {
            _context.Set<TModel>().Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        
    }
}