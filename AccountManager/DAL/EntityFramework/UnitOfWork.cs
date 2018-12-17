using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.DAL.EntityFramework
{
    internal class UnitOfWork : IUnitOfWork
    {
        //Atributos
        private IAccountRepository iAccountRepository;
        private IClientRepository iClientRepository;

        private AccountManagerDbContext iDbContext;
        private bool iDisposed = false;

        //Propiedades
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

        //Constructor

        public UnitOfWork()
        {
            iDbContext = new AccountManagerDbContext();
        }

        //Metodos

        public void Complete()
        {
            iDbContext.SaveChanges(); 
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.iDisposed)
            {
                if (disposing)
                {
                    iDbContext.Dispose();
                }
            }
            this.iDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
