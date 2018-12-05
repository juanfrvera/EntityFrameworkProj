using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.IO;
namespace AccountManager
{
    internal class Bank
    {
        public IEnumerable<AccountDTO> GetClientAccounts(int pClientId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<AccountMovementDTO> GetAccountMovements(int pAccountId)
        {
            throw new NotImplementedException();
        }
    }
}
