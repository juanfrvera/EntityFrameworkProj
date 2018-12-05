using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkProj
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (AgendaContext contexto = new AgendaContext())
                {
                    Persona persona = new Persona
                    {
                        Nombre = "Lucas",
                        Apellido = "Dilatro",
                        Telefonos = new List<Telefono>
                        { 
                            new Telefono
                            {
                                Numero = "555",
                                Tipo = "Fijo"
                            },
                            new Telefono
                            {
                                Numero = "1234",
                                Tipo = "Movil"
                            }
                        }
                    };
                    contexto.Personas.Add(persona);
                    contexto.SaveChanges();

                    foreach (Persona item in contexto.Personas)
                    {
                        Console.WriteLine($"{item.PersonaId} - {item.Apellido}, {item.Nombre}");
                    }
                    Console.WriteLine("Ejecucion finalizada");
                }
            }
            catch (Exception bfx)
            {
                Console.WriteLine($"Ha ocurrido un error: {bfx}");
            }
            Console.ReadKey();
        }
    }
}
