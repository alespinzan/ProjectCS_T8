using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLib;

namespace SimulatorConsole
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            // Tratar excepcion de formato incorrecto
            try
            {
                FlightPlanList lista = new FlightPlanList();
                // FlightPlan a
                Console.WriteLine("Escribe el identificador");
                //   string nombre = Console.ReadLine();
                string identificador = Console.ReadLine(); ;

                Console.WriteLine("Escribe la velocidad");
                double velocidad = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Escribe las coordenadas de la posición inicial, separadas por un blanco");
                string linea = Console.ReadLine();
                string[] trozos = linea.Split(' ');
                double ix = Convert.ToDouble(trozos[0]);
                double iy = Convert.ToDouble(trozos[1]);

                Console.WriteLine("Escribe las coordenadas de la posición final, separadas por un blanco");
                linea = Console.ReadLine();
                trozos = linea.Split(' ');
                double fx = Convert.ToDouble(trozos[0]);
                double fy = Convert.ToDouble(trozos[1]);


                // Instancia un objeto del tipo FlightPlan
                FlightPlan plan_a = new FlightPlan(identificador, ix, iy, fx, fy, velocidad);

                // FlightPlan b
                Console.WriteLine("Escribe el identificador");
                //   string nombre = Console.ReadLine();
                identificador = Console.ReadLine(); ;

                Console.WriteLine("Escribe la velocidad");
                velocidad = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Escribe las coordenadas de la posición inicial, separadas por un blanco");
                linea = Console.ReadLine();
                trozos = linea.Split(' ');
                ix = Convert.ToDouble(trozos[0]);
                iy = Convert.ToDouble(trozos[1]);

                Console.WriteLine("Escribe las coordenadas de la posición final, separadas por un blanco");
                linea = Console.ReadLine();
                trozos = linea.Split(' ');
                fx = Convert.ToDouble(trozos[0]);
                fy = Convert.ToDouble(trozos[1]);


                // Instancia un objeto del tipo FlightPlan
                FlightPlan plan_b = new FlightPlan(identificador, ix, iy, fx, fy, velocidad);

                lista.AddFlightPlan(plan_a);
                lista.AddFlightPlan(plan_b);

                // Simulación
                // Bucle de simulación (ciclos + tiempo de ciclos)

                int ciclos = 100;
                int tiempoCiclo = 10;
                double distanciaSeguridad = 10;

                int i = 0;
                while (i < ciclos)
                {
                    lista.Mover(tiempoCiclo);
                   
                  
                    lista.EscribeConsola();
                    
                    if (plan_a.Conflicto(plan_b, distanciaSeguridad))
                        Console.WriteLine("Conflicto!!!");
                    i = i + 1;
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Error de formato. ");
            }

            Console.ReadLine();

        }
    }
}
