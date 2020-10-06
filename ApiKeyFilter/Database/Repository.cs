using System.Collections.Generic;
using System.Linq;
using ApiKeyFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyFilter.Database {
    public class Repository<TModel> : IRepository<TModel> where TModel : ModelBase {
        private readonly DbSet<TModel> _dbSet;
        private readonly DbContext _context;

        public Repository(DbSet<TModel> dbSet, DbContext context) {
            _dbSet = dbSet;
            _context = context;
        }

        public TModel Get(int id) => _dbSet.FirstOrDefault(d => d.Id == id);

        public IQueryable<TModel> Get() => _dbSet;

        public TModel Add(TModel model) {
            _dbSet.Add(model);
            _context.SaveChanges();
            return model;
        }

        public TModel Update(TModel model) {
            _dbSet.Update(model);
            _context.SaveChanges();
            return model;
        }

        public void Delete(TModel model) {
            _dbSet.Remove(model);
            _context.SaveChanges();
        }

        public void Delete(int id) => Delete(Get(id));
    }
}
