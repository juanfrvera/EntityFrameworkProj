using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Domain
{
    internal class Client
    {
        //Atributos y propiedades
        private int id;

        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        private String firstName;

        public String FirstName
        {
            get { return firstName; }
            private set { firstName = value; }
        }

        private String lastName;

        public String LastName
        {
            get { return lastName; }
            private set { lastName = value; }
        }

        //Atributos y propiedades inferidos
        private Document iDocument;
        public Document Document { get { return iDocument; } }
        private IList<Account> iAccounts;
        public IList<Account> Accounts { get{return iAccounts;} }

        //Constructor
        public Client()
        {
            iAccounts = new List<Account>();
        }
        public Client(int pId, string pFirstName, string pLastName, string pDocumentType, string pDocumentNumber)
        {
            this.Id = pId;
            this.FirstName = pFirstName;
            this.LastName = pLastName;
            this.iDocument = new Document(pDocumentType,pDocumentNumber);
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