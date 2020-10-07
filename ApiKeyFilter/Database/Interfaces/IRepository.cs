using System.Linq;
using ApiKeyFilter.Models;

namespace ApiKeyFilter.Database.Interfaces {
    public interface IRepository<TModel> where TModel : ModelBase {
        TModel Get(int id);
        IQueryable<TModel> Get();
        TModel Add(TModel model);
        TModel Update(TModel model);
        void Delete(TModel model);
        void Delete(int id);
    }
}
