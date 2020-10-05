using System.Collections.Generic;
using System.Linq;
using ApiKeyFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyFilter.Database {
    public class Repository<TModel> : IRepository<TModel> where TModel : ModelBase {
        private readonly DbSet<TModel> _dbSet;
        private readonly IUnitOfWork _unitOfWork;

        public Repository(DbSet<TModel> dbSet, IUnitOfWork unitOfWork) {
            _dbSet = dbSet;
            _unitOfWork = unitOfWork;
        }

        public TModel Get(int id) => _dbSet.FirstOrDefault(d => d.Id == id);

        public IQueryable<TModel> Get() => _dbSet;

        public TModel Add(TModel model) {
            _dbSet.Add(model);
            _unitOfWork.SaveChanges();
            return model;
        }

        public void Delete(TModel model) {
            throw new System.NotImplementedException();
        }

        public void Delete(int id) => Delete(Get(id));
    }
}
