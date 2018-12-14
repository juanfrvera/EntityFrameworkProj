﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Domain
{
    internal class Client
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String firstName;

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private String lastName;

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        //Atributos inferidos
        private Document iDocument;
        private IList<Account> iAccounts;
        public IList<Account> Accounts { get{return iAccounts;} }

        //Constructor
        public Client()
        {
            iAccounts = new List<Account>();
        }

        /// <summary>
        /// Añade una cuenta a la lista de cuentas del cliente.
        /// </summary>
        /// <param name="pAccountMovement"></param>
        public void AddAccount(Account pAccount)
        {
            iAccounts.Add(pAccount);
        }
    }
}