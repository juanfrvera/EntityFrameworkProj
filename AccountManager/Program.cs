using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Bank controllerBank = new Bank();
            
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
            //ClientDTO client = new ClientDTO();

            //Dejo todas las asignaciones comentadas.
            Console.Write("Ingrese nombre: ");
            // client.firstName = Console.ReadLine();
            Console.Write("Ingrese apellido: ");
            // client.lastName = Console.ReadLine();
            Console.Write("Ingrese tipo de documento: ");
            Console.Write("1- DNI; 2- CUIL; 3- LE; 4- LC");
            // client.documentType = Console.ReadLine();  //Esto obvimente esta sujeto a cambio. estoy improvisando.
            Console.Write("Ingrese numero de documento: ");
            // client.documentNumber = Console.ReadLine();

            //controllerBank.CreateClient(client)   

            //Es decir, el clientDTO tendria campos id, firstName, lastName, documentType y documentNumber
            //Con los primeros creamos el Cliente propiamente dicho, con los ultimos dos creamos el objeto Document que es anexo.
            //El id deberia pasarse null aca y asignarse en el controlador, para no darle esa responsabilidad a la interfaz
            //Pero debe ser un campo para poder mostrarlo mas facilmente por pantalla.
          }
          catch (Exception)
          {

          }   
        }

        static void MenuCuenta()
        {   
          try
          {Console.WriteLine("Menu Cuenta"); Console.WriteLine();
            //Quiza es medio rudimentario dejarlo asi, pero tampoco me gusta la idea de andar dando datos de clientes
            //a lo loco poniendo el id. No hay forma de verificar ese es el problema.


            Console.Write("Ingrese el id del cliente: ");
            int clientId = Convert.ToInt(Console.ReadLine());
            Console.Write("Ingrese nombre de la cuenta: ");
            string accountName = Console.ReadLine();
            Console.Write("Ingrese saldo de acuerdo");
            double accountOverdraftLimit = Convert.ToDouble(Console.ReadLine());
           

            controllerBank.CreateAccount(clientId, accountName, accountOverdraftLimit);   

            //Esto estaria funcionando, lo que me preocupa es como dije arriba, no hay forma de verificar el cliente.

          }
          catch (Exception)
          {

          }   
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
                Console.WriteLine("Cualquier tecla - Volver");
                string opcion = Console.ReadLine();
                Console.Clear();
                switch (opcion)
                {
                    case "1":
                      try
                      { 
                        Console.WriteLine("Ingrese el id de la cuenta:");
                        int propietaryAccountId= Convert.ToInt(Console.ReadKey());
                        Console.WriteLine("Ingrese el monto a debitar:");
                        double amount = Convert.ToDouble(Console.ReadKey());
                        controllerBank.Debit(propietaryAccountId, amount);
                            //Debit no hace save, hay que ver como arreglamos eso.
                        break;
                      }
                     catch (Exception)
                        {

                        } 
                    case "2":
                      try
                      {
                        Console.WriteLine("Ingrese el id de la cuenta:");
                        int propietaryAccountId = Convert.ToInt(Console.ReadKey());
                        Console.WriteLine("Ingrese el monto a acreditar:");
                        double amount = Convert.ToDouble(Console.ReadKey());
                        controllerBank.Acredit(propietaryAccountId, amount);
                            //Acredit no hace save, hay que ver como arreglamos eso.
                        break;
                      }
                      catch (Exception)
                      {

                      } 
                    case "3":
                       try
                       {
                         Console.WriteLine("Ingrese el id de la cuenta origen:");
                         int propietaryAccountId= Convert.ToInt(Console.ReadKey());

                         Console.WriteLine("Ingrese el id de la cuenta destino:");
                         int otherAccountId= Convert.ToInt(Console.ReadKey());

                         Console.WriteLine("Ingrese el monto a transferir:");
                         double amount = Convert.ToDouble(Console.ReadKey());

                         controllerBank.Transference(propietaryAccountId,otherAccountId, amount);
                            //Transference si hace saveChanges.
                        break;
                       }
                        catch (Exception)
                       {

                       } 
                    default:
                        salir = true;
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
          try
          {
            Console.WriteLine("Menu Informacion Sumaria"); Console.WriteLine();
            
            //Obtenemos la lista de cuentas
            Console.Write("Ingrese el id del cliente: ");
            int clientId = Convert.ToInt(Console.ReadLine());
            IList<AccountDTO> accountList = controllerBank.getClientAccounts(clientId);
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
               
          }
          catch (Exception)
          {

          }   
        }

        static void MenuUltimos()
        {   
          try
          {
            Console.WriteLine("Menu Ultimos Movimientos"); Console.WriteLine();
            
            //Obtenemos la lista de movimientos
            Console.Write("Ingrese el id de la cuenta: ");
            int accountId = Convert.ToInt(Console.ReadLine());
            IList<AccountMovementDTO> novementList = controllerBank.getLastAccountMovements(accountId);
            Console.WriteLine();
            
            //Listamos su informacion
            Console.WriteLine("Movimientos:");    
            foreach (AccountMovementDTO movement in accountList)
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
