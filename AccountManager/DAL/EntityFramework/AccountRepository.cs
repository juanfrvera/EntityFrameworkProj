using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Domain;

namespace AccountManager.DAL.EntityFramework
{
    internal class AccountRepository : Repository<Account, AccountManagerDbContext>, IAccountRepository
    {
        public AccountRepository(AccountManagerDbContext pContext) : base(pContext){}

        public IEnumerable<Account> GetOverdrawnAccounts()
        {
            var accounts = base.GetAll();
            return accounts.Where(acc => acc.GetBalance() < 0);
        }
    }
}
