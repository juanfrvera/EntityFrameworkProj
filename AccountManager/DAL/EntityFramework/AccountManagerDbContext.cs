using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Domain;

namespace AccountManager.DAL.EntityFramework
{
    internal class AccountManagerDbContext : DbContext
    {
        public DbSet<Client> Clients{ get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountMovement> AccountMovements { get; set; }
    }
}
