using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Domain;

namespace AccountManager.DAL
{
    interface IAccountRepository : IRepository<Account>
    {
        IEnumerable<Account> GetOverdrawnAccounts();
    }
}