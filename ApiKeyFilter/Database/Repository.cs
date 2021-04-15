using System;
using System.Collections.Generic;
using System.Linq;
using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyFilter.Database {
    public class Repository<TModel> : IRepository<TModel> where TModel : ModelBase {
        protected readonly List<TModel> DbSet;
        protected readonly Func<List<TModel>, IQueryable<TModel>> GetIncludeSingle;
        protected readonly Func<List<TModel>, IQueryable<TModel>> GetIncludeAll;

        public Repository(List<TModel> dbSet, DbContext context,
            Func<List<TModel>, IQueryable<TModel>> getIncludeSingle = null,
            Func<List<TModel>, IQueryable<TModel>> getIncludeAll = null) {
            DbSet = dbSet;
            //Context = context;
            GetIncludeSingle = getIncludeSingle;
            GetIncludeAll = getIncludeAll;
        }

        public virtual TModel Get(string id) =>
            (GetIncludeSingle == null
                ? new EnumerableQuery<TModel>(DbSet)
                : GetIncludeSingle(DbSet)).FirstOrDefault(
                d => d.Id == id);

        public virtual IQueryable<TModel> Get() =>
            (GetIncludeAll == null ? new EnumerableQuery<TModel>(DbSet) : GetIncludeAll(DbSet))
            .Where(entry =>
                entry.Deleted == null);

        public virtual TModel Add(TModel model) {
            DbSet.Add(model);
            return model;
        }

        public virtual TModel Update(TModel model) {
            var entity = DbSet.FirstOrDefault((i => i.Id == model.Id));
            entity = model;
            return entity;
        }

        public virtual void Delete(TModel model, bool hardRemove) {
            if (hardRemove)
                DbSet.Remove(model);
            else
                model.Deleted = DateTime.Now;
        }

        public virtual void Delete(string id, bool hardRemove) => Delete(Get(id), hardRemove);
    }
}
