using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.IO;
using AccountManager.Domain;   //Este lo agregue yo recien
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

        public void CreateAccount(int pClientId,string pAccountName, double pAccountOverdraftLimit)
        { 
            //¿Como consigo los repositorios? Por ahora supongamos que uso estos comentarios como guia.

            // Client client = ClientRepository.get(pClientId);
            Client client = new Client(); //Esto lo pongo para lo que sigue, despues se borra.

            //¿Como conseguimos el id? Por ahora supongamos que lo consigo de x forma y lo asigno a una variable
            int accountId = 1;

            //Continuemos con lo que si seria el cuerpo
            Account account = new Account(accountId, pAccountName,pAccountOverdraftLimit, client);
            //accountRepository.add(account);    Supongamos que con eso queda añadido.


            //De esta forma, si la lista del cliente se crea por Mapeo no hace falta actualizarlo, solo
            // es necesario actualizar al repositorio de cuentas.
  
        }       
    }
}

