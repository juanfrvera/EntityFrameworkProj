using System;
using System.Collections.Generic;
using AccountManager.IO;
using AccountManager.Domain;
using AccountManager.DAL;
namespace AccountManager
{
    internal class Bank
    {
        private IUnitOfWork unitOfWork;

        public Bank(){

        }

        //Dado un cliente, se debe permitir obtener información sumaria de sus cuentas. 
        //Asumimos que pasan el id del cliente en vez del cliente entero ya que sino habria dependencia con
        //la clase Client desde afuera, ademas seria innecesario este metodo    
        public IEnumerable<AccountDTO> GetClientAccounts(int pClientId)
        {
            //Obtenemos el cliente por ID
            Client client = unitOfWork.ClientRepository.Get(pClientId);
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
        /// <summary>
        /// Traduce una lista que contiene AccountMovement y devuelve una lista que contiene AccountMovementDTO
        /// </summary>
        /// <param name="movements"></param>
        /// <returns></returns>
        private IEnumerable<AccountMovementDTO> Translate(IEnumerable<AccountMovement> movements)
        {
            //Es IList para poder usar el metodo "Add()"
            IList<AccountMovementDTO> accountMovementDTOs = new List<AccountMovementDTO>();

            foreach (AccountMovement mov in movements)
            {
                AccountMovementDTO dTO = new AccountMovementDTO();
                dTO.Id = mov.Id;
                dTO.Date = mov.Date;
                dTO.Description = mov.Description;
                dTO.Amount = mov.Amount;

                accountMovementDTOs.Add(dTO);
            }
            //Devolvemos la lista de dtos
            return accountMovementDTOs;
        }

        public IEnumerable<AccountMovementDTO> GetAccountMovements(int pAccountId)
        {
            try 
            {	        
               Account account = unitOfWork.AccountRepository.Get(pAccountId);

               return Translate(account.GetMovements());
            }
            //Tenemos que analizar las excepciones correspondientes a cuenta no encontrada y las de error en la base de datos
            catch (Exception)
            {
	            throw;
            }
        }

        public IEnumerable<AccountMovementDTO> GetLastAccountMovements(int pAccountId, int pCount)
        {
            try 
            {	        
               Account account = unitOfWork.AccountRepository.Get(pAccountId);

               return Translate(account.GetLastMovements(pCount));
            }
            //Tenemos que analizar las excepciones correspondientes a cuenta no encontrada y las de error en la base de datos
            catch (Exception)
            {
	            throw;
            }
        }

        public void CreateAccount(int pClientId,string pAccountName, double pAccountOverdraftLimit)
        { 
            try 
            {	        
                Client client = unitOfWork.ClientRepository.Get(pClientId);

                int accountId = unitOfWork.AccountRepository.Count();

                Account account = new Account(accountId, pAccountName, pAccountOverdraftLimit, client);

                //Añadimos la cuenta al cliente
                client.AddAccount(account);
                //Añadimos la cuenta y guardamos los cambios.
                unitOfWork.AccountRepository.Add(account);
                unitOfWork.Complete();
              
            }
            //Tenemos que analizar las excepciones correspondientes a cliente no encontrado y las de error en la base de datos
            catch (Exception)
            {
	            throw;
            }
        }       


        //Registrar los movimientos realizados en las cuentas, para los cuales se debe
        //registrar la fecha en que fue realizado el movimiento, la descripción del mismo y la
        //cantidad de dinero acreditado o debitado de la cuenta.
        //{Agregamos este metodo ya que pide registrar movimientos.} 
        public void AddNewMovement(int pAccountId, AccountMovementDTO pMovDTO)
        {
            try 
            {	
                //Buscamos la cuenta y creamos un movimiento nuevo con los datos del parametro.      
                Account account = unitOfWork.AccountRepository.Get(pAccountId);

                AccountMovement movement = new AccountMovement(pMovDTO.Id, pMovDTO.Date, pMovDTO.Description, pMovDTO.Amount);

                //Agregamos el movimiento a la cuenta.
                account.AddMovement(movement);

                //Faltaria "guardar" la modificacion. Deberiamos preguntar y sino borrar.
                unitOfWork.Complete();
            }

            //Tenemos que analizar las excepciones correspondientes a cuenta no encontrada y las de error en la base de datos
            catch (Exception)
            {
	            throw;
            }
        }

        public void Accredit(int pAccountId, double pAmount)
        {
            try
            {
                Account account = unitOfWork.AccountRepository.Get(pAccountId);
                account.AddMovement(new AccountMovement(pAccountId, DateTime.Now, "Accredit", pAmount));
            }
            //Excepcion de cuenta no encontrada
            catch (Exception)
            {

                throw;
            }
        }
        public void Debit(int pAccountId, double pAmount)
        {
            try
            {
                Account account = unitOfWork.AccountRepository.Get(pAccountId);
                account.AddMovement(new AccountMovement(pAccountId, DateTime.Now, "Debit", -pAmount));
            }
            //Excepciones de cuenta no encontrada y de saldo insuficiente
            catch (Exception)
            {
                throw;
            }
        }

        public void Transference(int pSenderAccountId, int pRecieverAccountId, double pAmount)
        {
            try
            {   //Debitamos de la cuenta origen
                this.Debit(pSenderAccountId, pAmount);

                //Acreditamos en la cuenta destino
                this.Accredit(pRecieverAccountId, pAmmount);


                //Al terminar todo hacemos un "Complete" en el unit of work para que se guarde todo o nada.
                unitOfWork.Complete();
            }
            //Excepciones de sender o receiver no encontrados
            //Excepcion de saldo insuficiente en sender
            catch (Exception)
            {
	            throw;
            }
        }
    }
}