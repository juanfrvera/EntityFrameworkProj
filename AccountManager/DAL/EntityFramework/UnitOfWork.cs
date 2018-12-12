using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.DAL.EntityFramework
{
    internal class UnitOfWork : IUnitOfWork
    {
        AccountManagerDbContext iDbContext;



        public IAccountRepository AccountRepository
        {
            get
            {
                if (this.AccountRepository == null)
                {
                    this.AccountRepository = new AccountRepository(context);
                }
                return AccountRepository;
            }
        }

        public IClientRepository ClientRepository 
        {
            get
            {
                if (this.ClientRepository == null)
                {
                    this.ClientRepository = new ClientRepository(context);
                }
                return ClientRepository;
            }
        }

        public void Complete()
        {
           context.SaveChanges(); 
        }

        private bool disposed = false;

        private virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
