using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] vector = new FlightPlan[20];
        int number = 0;
        string name = string.Empty;

        public int getnum()
        {
            return number;
        }

        public void setname(string name)
        {
            this.name = name;
        }

        public string getname()
        {
            return name;
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
            name = string.Empty;
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

                if (lines.Length == 0)
                {
                    return -1;
                }

                this.setname(Path.GetFileNameWithoutExtension(filePath));

                foreach (string rawLine in lines)
                {
                    if (number >= vector.Length) break;

                    string line = rawLine.Trim();
                    if (line.Length == 0 || line.StartsWith("#")) continue;

                    string[] parts = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 8) continue;

                    if (!double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double originX) ||
                        !double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double originY) ||
                        !double.TryParse(parts[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double currentX) ||
                        !double.TryParse(parts[4], NumberStyles.Float, CultureInfo.InvariantCulture, out double currentY) ||
                        !double.TryParse(parts[5], NumberStyles.Float, CultureInfo.InvariantCulture, out double destX) ||
                        !double.TryParse(parts[6], NumberStyles.Float, CultureInfo.InvariantCulture, out double destY) ||
                        !double.TryParse(parts[7], NumberStyles.Float, CultureInfo.InvariantCulture, out double velocity))
                    {
                        continue;
                    }

                    string id = parts[0];
                    FlightPlan newPlan = new FlightPlan(id, originX, originY, destX, destY, velocity);
                    newPlan.SetCurrentPosition(new Position(currentX, currentY));

                    if (AddFlightPlan(newPlan) == 0)
                    {
                        plansAdded++;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return plansAdded;
        }

        public int saveToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int i = 0; i < number; i++)
                    {
                        FlightPlan plan = vector[i];
                        Position origin = plan.GetInitialPosition();
                        Position current = plan.GetCurrentPosition();
                        Position destination = plan.GetFinalPosition();

                        // Using InvariantCulture to ensure '.' is used as the decimal separator
                        string line = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                            "{0} {1} {2} {3} {4} {5} {6} {7}",
                            plan.GetId(),
                            origin.GetX(),
                            origin.GetY(),
                            current.GetX(),
                            current.GetY(),
                            destination.GetX(),
                            destination.GetY(),
                            plan.GetVelocidad());

                        writer.WriteLine(line);
                    }
                }
                return 0; // Success
            }
            catch (Exception)
            {
                // Could be a path error, access denied, etc.
                return -1; // General error
            }
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

        public void RemoveLast()
        {
            if (number > 0)
            {
                number--;
                vector[number] = null; 
            }
        }

        public void RemoveById(string id)
        {
            int foundIndex = -1;
            for (int i = 0; i < number; i++)
            {
                if (vector[i].GetId() == id)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex != -1)
            {
                
                for (int i = foundIndex; i < number - 1; i++)
                {
                    vector[i] = vector[i + 1];
                }
                number--;
                vector[number] = null; 
            }
        }

    }
}
