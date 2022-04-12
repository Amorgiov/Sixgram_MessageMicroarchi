using System;
using System.Linq;
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
            => await _context.Set<TModel>().AsNoTracking().FirstAsync(p => p.Id == id);

        public async Task<TModel> Delete(TModel item)
        {
            _context.Set<TModel>().Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
        
        
    }
}