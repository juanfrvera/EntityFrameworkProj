using AccountManager.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager
{
    class Program
    {
        private static Bank controllerBank;

        static void Main(string[] args)
        {
            try
            {
                controllerBank = new Bank();
            
                 while (true)
                 {
                     Console.Clear();
                     Console.WriteLine("Menu Principal");Console.WriteLine();
                     Console.WriteLine("1-Agregar Cliente");
                     Console.WriteLine("2-Agregar Cuenta");
                     Console.WriteLine("3-Realizar movimiento");
                     Console.WriteLine("4-Listar informacion sumaria");
                     Console.WriteLine("5-Listar ultimos movimientos");
                     string opcion = Console.ReadLine();
                     switch (opcion)
                     {
                       case "1":
                        Program.MenuCliente();
                        break;
                       case "2":
                        Program.MenuCuenta();
                        break;
                       case "3":
                        Program.MenuMovimiento();
                        break;
                       case "4":
                        Program.MenuSumaria();
                        break;
                       case "5":
                        Program.MenuUltimos();
                        break;
                      default:
                        Console.WriteLine("Opcion incorrecta");
                        Console.ReadKey();
                        break;
                     }
                 }
            }
            catch (Exception)
            {

            }
        }

        static void MenuCliente()
        {   
            try
            {
                ClientDTO client = new ClientDTO();

                //Dejo todas las asignaciones comentadas.
                Console.Write("Ingrese nombre: ");
                client.firstName = Console.ReadLine();
                Console.Write("Ingrese apellido: ");
                client.lastName = Console.ReadLine();
                Console.Write("Ingrese tipo de documento: ");
                Console.Write("1- DNI; 2- CUIL; 3- LE; 4- LC");
                int type = 0;
                do
                {
                    type = Convert.ToInt16(Console.ReadLine());
                    if (type < 1 || type > 4)
                        Console.WriteLine("Por favor ingrese un valor entre 1 y 4.");
                    } while (type < 1 || type > 4);
                    switch (type)
                    {
                    case 1:
                        client.documentType = "DNI";
                        break;
                    case 2:
                        client.documentType = "CUIL";
                        break;
                    case 3:
                        client.documentType = "LE";
                        break;
                    case 4:
                        client.documentType = "LC";
                        break;
                    default:
                        break;
                }
                Console.Write("Ingrese numero de documento: ");
                client.documentNumber = Console.ReadLine();

                controllerBank.CreateClient(client);
            }
            catch (Exception)
            {

            }   
        }

        static void MenuCuenta()
        {   
            Console.WriteLine("Menu Cuenta"); Console.WriteLine();
            //Quiza es medio rudimentario dejarlo asi, pero tampoco me gusta la idea de andar dando datos de clientes
            //a lo loco poniendo el id. No hay forma de verificar ese es el problema.
            try
            {
                Console.Write("Ingrese el id del cliente: ");
                int clientId = Convert.ToInt16(Console.ReadLine());
                Console.Write("Ingrese nombre de la cuenta: ");
                string accountName = Console.ReadLine();
                Console.Write("Ingrese saldo de acuerdo");
                double accountOverdraftLimit = Convert.ToDouble(Console.ReadLine());
                controllerBank.CreateAccount(clientId, accountName, accountOverdraftLimit);
            }
            catch (Bank.ClientDoesntExistsException)
            {
                Console.WriteLine("El Id de cliente no es valido.");
            }

            //Esto estaria funcionando, lo que me preocupa es como dije arriba, no hay forma de verificar el cliente.
        }

        static void MenuMovimiento()
        {   
          try
          {
            int propietaryAccountId;
            int otherAccountId;
            double amount; 
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("Menu Movimientos"); Console.WriteLine();
                Console.WriteLine("1-Debitar");
                Console.WriteLine("2-Acreditar");
                Console.WriteLine("3-Transferencia");
                Console.WriteLine("0 - Volver");
                int opcion = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                switch (opcion)
                {
                    case 0:
                        salir = true;
                        break;
                    case 1:
                        try
                        { 
                            Console.WriteLine("Ingrese el id de la cuenta:");
                            propietaryAccountId = Convert.ToInt16(Console.ReadLine());
                            Console.WriteLine("Ingrese el monto a debitar:");
                            amount = Convert.ToDouble(Console.ReadLine());
                            controllerBank.Debit(propietaryAccountId, amount);
                            //Debit no hace save, hay que ver como arreglamos eso.
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Ingrese el id de la cuenta:");
                            propietaryAccountId = Convert.ToInt16(Console.ReadKey());
                            Console.WriteLine("Ingrese el monto a acreditar:");
                            amount = Convert.ToDouble(Console.ReadKey());
                            controllerBank.Accredit(propietaryAccountId, amount);
                            //Acredit no hace save, hay que ver como arreglamos eso.
                        }
                        catch (Exception)
                        {

                        }
                        break;
                    case 3:
                        try
                        {
                        Console.WriteLine("Ingrese el id de la cuenta origen:");
                        propietaryAccountId= Convert.ToInt16(Console.ReadKey());

                        Console.WriteLine("Ingrese el id de la cuenta destino:");
                        otherAccountId = Convert.ToInt16(Console.ReadKey());

                        Console.WriteLine("Ingrese el monto a transferir:");
                        amount = Convert.ToDouble(Console.ReadKey());

                        controllerBank.Transference(propietaryAccountId,otherAccountId, amount);
                        //Transference si hace saveChanges.
                        }
                        catch (Exception)
                        {

                        }
                        break;
                    default:
                    break;
                } 
            }

          }
          catch (Exception)
          {

          }   
        }
        
        static void MenuSumaria()
        {   
          //try
          //{
            Console.WriteLine("Menu Informacion Sumaria"); Console.WriteLine();
            
            //Obtenemos la lista de cuentas
            Console.Write("Ingrese el id del cliente: ");
            int clientId = Convert.ToInt16(Console.ReadLine());
            IEnumerable<AccountDTO> accountList = controllerBank.GetClientAccounts(clientId);
            Console.WriteLine();
            
            //Listamos su informacion
            Console.WriteLine("Informacion sumaria:");    
            foreach (AccountDTO account in accountList)
	        {
                Console.WriteLine();
                Console.WriteLine("ID: ", account.Id);
                Console.WriteLine("Nombre: ", account.Name);
                Console.WriteLine("Acuerdo: ", account.OverdraftLimit);
                Console.WriteLine("Balance: ", account.Balance);
                Console.WriteLine();
	        }
            Console.ReadKey();
               
          //}
         // catch (Exception)
          //{
        //
          //}   
        }

        static void MenuUltimos()
        {   
          try
          {
            Console.WriteLine("Menu Ultimos Movimientos"); Console.WriteLine();
            
            //Obtenemos la lista de movimientos
            Console.Write("Ingrese el id de la cuenta: ");
            int accountId = Convert.ToInt16(Console.ReadLine());
            Console.Write("Ingrese la cantidad de movimientos que desea ver: ");
            int cantidad = Convert.ToInt16(Console.ReadLine());
            IEnumerable<AccountMovementDTO> movementList = controllerBank.GetLastAccountMovements(accountId,cantidad);
            Console.WriteLine();
            
            //Listamos su informacion
            Console.WriteLine("Movimientos:");    
            foreach (AccountMovementDTO movement in movementList)
	        {
                Console.WriteLine();
                Console.WriteLine("ID: ", movement.Id);
                Console.WriteLine("Fecha: ", movement.Date);
                Console.WriteLine("Descripcion: ", movement.Description);
                Console.WriteLine("Monto: ", movement.Amount);
                Console.WriteLine();
	        }
          }
          catch (Exception)
          {

          }   
        }
    }
}
