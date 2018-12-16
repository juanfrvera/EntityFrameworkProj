using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.DAL.EntityFramework
{
    internal class UnitOfWork : IUnitOfWork
    {
        IAccountRepository iAccountRepository;
        IClientRepository iClientRepository;

        AccountManagerDbContext iDbContext;


        public IAccountRepository AccountRepository
        {
            get
            {
                if (this.iAccountRepository == null)
                {
                    this.iAccountRepository = new AccountRepository(iDbContext);
                }
                return iAccountRepository;
            }
        }
        public IClientRepository ClientRepository 
        {
            get
            {
                if (this.iClientRepository == null)
                {
                    this.iClientRepository = new ClientRepository(iDbContext);
                }
                return iClientRepository;
            }
        }

        private UnitOfWork()
        {
            iDbContext = new AccountManagerDbContext();
        }


        public void Complete()
        {
            iDbContext.SaveChanges(); 
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    iDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
