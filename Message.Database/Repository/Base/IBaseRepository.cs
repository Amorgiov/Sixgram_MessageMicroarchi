using System;
using System.Threading.Tasks;
using Message.Common.Base;

namespace Message.Database.Repository.Base
{
    public interface IBaseRepository<TModel>
        where TModel : BaseModel
    {
        Task<TModel> Create(TModel data);
        Task<TModel> GetById(Guid id);
        TModel GetOne(Func<TModel, bool> predicate);
        Task<TModel> Update(TModel item);
        Task<TModel> Delete(TModel id);
    }
}