using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using AccountManager.DAL;
using System.Linq;

namespace AccountManager.DAL.EntityFramework
{
    internal class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class where TDbContext : DbContext
    {
        TDbContext iDbContext;
        //Constructor
        public Repository(TDbContext pContext)
        {
            this.iDbContext = pContext;
        }
        
        //Methods
        public void Add(TEntity pEntity)
        {
            iDbContext.Set<TEntity>().Add(pEntity);
        }

        public TEntity Get(int pId)
        {
            return iDbContext.Set<TEntity>().Find(pId);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return iDbContext.Set<TEntity>();
        }

        public void Remove(TEntity pEntity)
        {
            iDbContext.Set<TEntity>().Remove(pEntity);
        }

        public int Count()
        {
            return iDbContext.Set<TEntity>().Count<TEntity>();
        }

        public void SaveChanges(){
            iDbContext.SaveChanges();
        }
    }
}
