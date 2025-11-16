using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] vector = new FlightPlan[20];
        int number = 0;

        public int getnum()
        {
            return number;

        }
        

        public int AddFlightPlan(FlightPlan p)
        {
            if (number == 10)
            { return -1; }
            else
            {
                vector[number] = p;
                number++;
                return 0;
            }
        }

        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= number)
            { return null; }
            else
            {
                return vector[i];
            }


        }
        public void Mover(double tiempo)
        {
            int i = 0;
            while (i < number)
            {
                vector[i].Mover(tiempo);
                i++;
            }

        }
        public void Clear()
        {
            for (int i = 0; i < number; i++)
                vector[i] = null;
            number = 0;
        }

        public void EscribeConsola()
        {
            int i = 0;
            while (i < number)
            {
                vector[i].EscribeConsola();
                i++;
            }

        }

        public int loadFlpFromFile(string filePath)
        {
            int plansAdded = 0;
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (number >= vector.Length) break;

                    string[] parts = line.Split(',');
                    if (parts.Length == 6)
                    {
                        try
                        {
                            string id = parts[0].Trim();
                            double originX = Convert.ToDouble(parts[1].Trim());
                            double originY = Convert.ToDouble(parts[2].Trim());
                            double destX = Convert.ToDouble(parts[3].Trim());
                            double destY = Convert.ToDouble(parts[4].Trim());
                            double velocity = Convert.ToDouble(parts[5].Trim());

                            FlightPlan newPlan = new FlightPlan(id, originX, originY, destX, destY, velocity);

                            if (AddFlightPlan(newPlan) == 0)
                            {
                                plansAdded++;
                            }
                        }
                        catch (FormatException)
                        {
                            // Skip lines with incorrect number format
                            continue;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Could be file not found, access denied, etc.
                return -1; // Indicate a general error
            }
            return plansAdded;
        }

        public bool DetectConflict(double _securityDistance)
        {
            int numFlights = number;
            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = vector[i];
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = vector[j];
                    if (a.Conflicto(b, _securityDistance))
                    {
                        return true; // Solo muestra el primer conflicto encontrado en este ciclo
                    }
                }
            }
            return false; // Retorna falso si no se encuentran conflictos
        }

        public bool predictConflict(FlightPlan a, FlightPlan b, double securityDistance)
        {
            Position aStart = a.GetInitialPosition();
            Position aEnd = a.GetFinalPosition();
            Position bStart = b.GetInitialPosition();
            Position bEnd = b.GetFinalPosition();

            // --- CÁLCULO DE VECTORES DE DIRECCIÓN (NORMALIZADO) ---
            double distA = aStart.Distancia(aEnd);
            double distB = bStart.Distancia(bEnd);

            // Evitar división por cero si el vuelo no se mueve
            double cosA = (distA == 0) ? 0 : (aEnd.GetX() - aStart.GetX()) / distA;
            double sinA = (distA == 0) ? 0 : (aEnd.GetY() - aStart.GetY()) / distA;
            double cosB = (distB == 0) ? 0 : (bEnd.GetX() - bStart.GetX()) / distB;
            double sinB = (distB == 0) ? 0 : (bEnd.GetY() - bStart.GetY()) / distB;

            // --- CÁLCULO DE VECTOR DE VELOCIDAD (CORREGIDO) ---
            double vax = cosA * a.GetVelocidad();
            double vay = sinA * a.GetVelocidad();
            double vbx = cosB * b.GetVelocidad();
            double vby = sinB * b.GetVelocidad();

            // --- EL RESTO DE TU CÓDIGO (QUE AHORA FUNCIONARÁ) ---
            double rx = aStart.GetX() - bStart.GetX();
            double ry = aStart.GetY() - bStart.GetY();
            double vx = vax - vbx;
            double vy = vay - vby;

            double tMin = 0;
            double denom = vx * vx + vy * vy;

            // Comprobamos que las velocidades relativas no sean cero
            if (denom > 1e-6)
            {
                tMin = -(rx * vx + ry * vy) / denom;
                tMin = Math.Max(0, tMin); // Solo tiempos futuros
            }

            // Comprobar si el tiempo de colisión está dentro del tiempo de vuelo
            // (Si un vuelo tarda 10s en llegar y la colisión es en t=5000, no hay conflicto)
            double timeToA = (a.GetVelocidad() == 0) ? double.PositiveInfinity : distA / a.GetVelocidad();
            double timeToB = (b.GetVelocidad() == 0) ? double.PositiveInfinity : distB / b.GetVelocidad();

            // Si el tiempo de colisión es mayor que el tiempo que
            // tarda CUALQUIERA de los dos en llegar, no hay conflicto.
            if (tMin > timeToA || tMin > timeToB)
            {
                return false;
            }

            // Posiciones en el tiempo de mínima distancia (tMin)
            double aX = aStart.GetX() + vax * tMin;
            double aY = aStart.GetY() + vay * tMin;
            double bX = bStart.GetX() + vbx * tMin;
            double bY = bStart.GetY() + vby * tMin;

            double dist = Math.Sqrt((aX - bX) * (aX - bX) + (aY - bY) * (aY - bY));
            return dist < securityDistance;
        }

        public (bool, double) ResolveConflictBySpeed(FlightPlan a, FlightPlan b, double securityDistance)
        {
            double originalSpeed = b.GetVelocidad();
            double minSpeed = 0.1; // Minimum allowed speed (avoid zero)
            double step = originalSpeed / 20.0; // Try 20 steps

            for (double newSpeed = originalSpeed - step; newSpeed >= minSpeed; newSpeed -= step)
            {
                b.SetVelocidad(newSpeed);
                if (!predictConflict(a, b, securityDistance))
                {
                    return (true, newSpeed); // Conflict resolved
                }
            }
            b.SetVelocidad(originalSpeed); // Restore if not resolved
            return (false, originalSpeed);
        }


        // Método para comprobar conflictos entre todos los pares de vuelos
    }
}
