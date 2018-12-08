using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Domain
{
    internal class Account
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private double overdraftLimit;

        public double OverdraftLimit
        {
            get { return overdraftLimit; }
            set { overdraftLimit = value; }
        }

        //Atributos inferidos
        private Client iClient;

        public Client Client
        {
            get { return client; }
            set { client = value; } 
        }

        private IList<AccountMovement> iMovements;

  
        public Account(int pId, string pName, double pOverdraftLimit, Client pClient)
        {
            id = pId;
            name = pName;
            overdraftLimit = pOverdraftLimit;
            client = pClient; 
            iMovements = new List<AccountMovement>();
        }
        //Metodos
        public double GetBalance()
        {
            double balance = 0;
            foreach (AccountMovement mov in iMovements)
            {
                balance += mov.Amount;
            }
            return balance;
        }
        public IEnumerable<AccountMovement> GetLastMovements(int pCount = 7)
        {   
            return iMovements.OrderByDescending(mov => mov.Date).Take<AccountMovement>(pCount);
        }

          public IEnumerable<AccountMovement> GetMovements()
        {   
            return iMovements.OrderByDescending(mov => mov.Date);
        }
    }
}