using System.Linq;
using ApiKeyFilter.Models;

namespace ApiKeyFilter.Database.Interfaces {
    public interface IRepository<TModel> where TModel : ModelBase {
        TModel? Get(string id);
        IQueryable<TModel> Get();
        TModel Add(TModel model);
        TModel Update(TModel model);
        void Delete(TModel model, bool hardRemove);
        void Delete(string id, bool hardRemove);
    }
}
