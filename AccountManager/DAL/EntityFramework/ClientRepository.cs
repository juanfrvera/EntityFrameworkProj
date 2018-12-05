using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Domain;

namespace AccountManager.DAL.EntityFramework
{
    class ClientRepository : Repository<Client, AccountManagerDbContext>, IClientRepository
    {
        public ClientRepository(AccountManagerDbContext pContext) : base(pContext){}
    }
}
