using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] vector = new FlightPlan[10];
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

        public bool detectConflict(double _securityDistance)
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

            // Vector direction
            double ax = aEnd.GetX() - aStart.GetX();
            double ay = aEnd.GetY() - aStart.GetY();
            double bx = bEnd.GetX() - bStart.GetX();
            double by = bEnd.GetY() - bStart.GetY();

            // Relative velocity
            double vax = ax * a.GetVelocidad();
            double vay = ay * a.GetVelocidad();
            double vbx = bx * b.GetVelocidad();
            double vby = by * b.GetVelocidad();

            // Relative position and velocity
            double rx = aStart.GetX() - bStart.GetX();
            double ry = aStart.GetY() - bStart.GetY();
            double vx = vax - vbx;
            double vy = vay - vby;

            // Find time t where distance is minimized
            double tMin = 0;
            double denom = vx * vx + vy * vy;
            if (denom != 0)
            {
                tMin = -(rx * vx + ry * vy) / denom;
                tMin = Math.Max(0, tMin); // Only future times
            }

            // Positions at tMin
            double aX = aStart.GetX() + ax * a.GetVelocidad() * tMin;
            double aY = aStart.GetY() + ay * a.GetVelocidad() * tMin;
            double bX = bStart.GetX() + bx * b.GetVelocidad() * tMin;
            double bY = bStart.GetY() + by * b.GetVelocidad() * tMin;

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
