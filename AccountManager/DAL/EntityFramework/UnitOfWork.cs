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



        public IAccountRepository AccountRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IClientRepository ClientRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Complete()
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
