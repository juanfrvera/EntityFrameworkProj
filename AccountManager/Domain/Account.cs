﻿using System;
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
            get { return iClient; }
            set { iClient = value; } 
        }

        private IList<AccountMovement> iMovements;

  
        public Account(int pId, string pName, double pOverdraftLimit, Client pClient)
        {
            id = pId;
            name = pName;
            overdraftLimit = pOverdraftLimit;
            iClient = pClient; 
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
        /// <summary>
        /// Devuelve los ultimos movimientos que hubo en la cuenta
        /// </summary>
        /// <param name="pCount"></param>
        /// <returns></returns>
        public IEnumerable<AccountMovement> GetLastMovements(int pCount = 7)
        {   
            return iMovements.OrderByDescending(mov => mov.Date).Take(pCount);
        }
        /// <summary>
        /// Devuelve todos los movimientos registrados en la cuenta
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AccountMovement> GetMovements()
        {   
            return iMovements.OrderByDescending(mov => mov.Date);
        }

        public class InsufficientBalanceException : Exception
        {
            public InsufficientBalanceException() : base("Insufficient balance"){}
        }
        /// <summary>
        /// Registra el movimiento indicado en la cuenta
        /// </summary>
        /// <param name="pAccountMovement"></param>
        public void AddMovement(AccountMovement pAccountMovement)
        {
            double balance = this.GetBalance();
            if (balance + pAccountMovement.Amount >= - this.overdraftLimit)
            {
                iMovements.Add(pAccountMovement);
            }
            else
            { throw new InsufficientBalanceException(); }
        }


        //Metodos para las transferencias.
        public void Accredit(AccountMovement pAccountMovement)
        {
            try
            {
                this.AddMovement(pAccountMovement);
            }
            //Excepcion de cuenta no encontrada
            catch (Exception)
            {

                throw;
            }
        }
        public void Debit(AccountMovement pAccountMovement)
        {
            try
            {
                this.AddMovement(pAccountMovement);
            }
            //Excepciones de cuenta no encontrada y de saldo insuficiente
            catch (Exception)
            {
                throw;
            }
        }
    }
}