using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Domain
{
    internal class AccountMovement
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        private double amount;

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        //Constructor
        public AccountMovement(int pId, DateTime pDate, string pDescription, double pAmmount)
        {
            this.Id = pId;
            this.Date = pDate;
            this.Description = pDescription;
            this.Amount = pAmmount;
        }
    }
}