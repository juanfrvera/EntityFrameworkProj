using System;
using System.Collections.Generic;
using AccountManager.IO;
using AccountManager.Domain;
using AccountManager.DAL;
namespace AccountManager
{
    internal class Bank
    {
        private IClientRepository clientRepository;
        private IAccountRepository accountRepository;

        public IEnumerable<AccountDTO> GetClientAccounts(int pClientId)
        {
            //Obtenemos el cliente por ID
            Client client = clientRepository.Get(pClientId);
            //Obtenemos todas las cuentas del cliente
            IList<Account> accounts = client.Accounts;
            //Creamos la lista que vamos a devolver y le decimos la capacidad que va a tener para que no
            //tenga que crecer dinamicamente.
            IList<AccountDTO> accountDTOs = new List<AccountDTO>(accounts.Count);
            foreach (Account acc in accounts)
            {
                AccountDTO dTO = new AccountDTO();
                dTO.Id = acc.Id;
                dTO.Name = acc.Name;
                dTO.OverdraftLimit = acc.OverdraftLimit;
                dTO.Balance = acc.GetBalance();

                accountDTOs.Add(dTO);
            }
            //Devolvemos la lista de dtos
            return accountDTOs;
        }
        public IEnumerable<AccountMovementDTO> GetAccountMovements(int pAccountId)
        {
            throw new NotImplementedException();
        }

        public void CreateAccount(int pClientId,string pAccountName, double pAccountOverdraftLimit)
        { 
            try 
            {	        
                Client client = clientRepository.Get(pClientId);

                int accountId = accountRepository.Count();

                Account account = new Account(accountId, pAccountName, pAccountOverdraftLimit, client);

                accountRepository.Add(account);
                //De esta forma, si la lista del cliente se crea por Mapeo no hace falta actualizarlo, solo
                // es necesario actualizar al repositorio de cuentas.
            }
            //Tenemos que analizar las excepciones correspondientes a cliente no encontrado y las de error en la base de datos
            catch (Exception)
            {
	            throw;
            }
        }       
        //Dado un cliente, se debe permitir obtener información sumaria de sus cuentas. 
        //Asumimos que pasan el id del cliente en vez del cliente entero ya que sino habria dependencia con
        //la clase Client desde afuera, ademas seria innecesario este metodo
      
}