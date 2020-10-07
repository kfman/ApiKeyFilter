using System;
using System.Collections.Generic;
using System.Linq;
using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyFilter.Database {
    public class Repository<TModel> : IRepository<TModel> where TModel : ModelBase {
        protected readonly DbSet<TModel> DbSet;
        protected readonly DbContext Context;
        protected readonly Func<DbSet<TModel>, IQueryable<TModel>> GetIncludeSingle;
        protected readonly Func<DbSet<TModel>, IQueryable<TModel>> GetIncludeAll;

        public Repository(DbSet<TModel> dbSet, DbContext context,
            Func<DbSet<TModel>, IQueryable<TModel>> getIncludeSingle = null,
            Func<DbSet<TModel>, IQueryable<TModel>> getIncludeAll = null) {
            DbSet = dbSet;
            Context = context;
            GetIncludeSingle = getIncludeSingle;
            GetIncludeAll = getIncludeAll;
        }

        public virtual TModel Get(string id) =>
            (GetIncludeAll == null ? DbSet : GetIncludeSingle(DbSet)).FirstOrDefault(
                d => d.Id == id);

        public virtual IQueryable<TModel> Get() =>
            GetIncludeAll == null ? DbSet : GetIncludeAll(DbSet);

        public virtual TModel Add(TModel model) {
            DbSet.Add(model);
            Context.SaveChanges();
            return model;
        }

        public virtual TModel Update(TModel model) {
            DbSet.Update(model);
            Context.SaveChanges();
            return model;
        }

        public virtual void Delete(TModel model) {
            DbSet.Remove(model);
            Context.SaveChanges();
        }

        public virtual void Delete(string id) => Delete(Get(id));
    }
}
