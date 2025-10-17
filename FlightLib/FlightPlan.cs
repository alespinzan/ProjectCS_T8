using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
    {
        // Atributos
        string id; // identificador
        Position initialPosition;
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        double velocidad;

        // Constructores
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
        }

        public FlightPlan()
        {
        }

        //Get y Set para cada atributo
        public string GetId() { return id; }
        public void SetId(string value) { id = value; }

        public Position GetInitialPosition() { return initialPosition; }
        public void SetInitialPosition(Position value) { initialPosition = value; }

        public Position GetCurrentPosition() { return currentPosition; }
        public void SetCurrentPosition(Position value) { currentPosition = value; }

        public Position GetFinalPosition() { return finalPosition; }
        public void SetFinalPosition(Position value) { finalPosition = value; }

        public double GetVelocidad() { return velocidad; }
        public void SetVelocidad(double value) { velocidad = value; }

        // Mueve el vuelo a la posición correspondiente a viajar durante el tiempo que se recibe como parámetro

        public void Mover(double tiempo)
        {
            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            //Calculamos las razones trigonométricas
            double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
            if (hipotenusa == 0) return; // Ya está en destino
            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            //Caculamos la nueva posición del vuelo
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            Position nextPosition = new Position(x, y);

            // Modificar MoverVuelo para que no se pase del destino
            if (currentPosition.Distancia(nextPosition) < hipotenusa)
                currentPosition = nextPosition;
            else
                currentPosition = finalPosition;
        }

        public void Restart()
        {
            currentPosition = new Position(initialPosition.GetX(), initialPosition.GetY());
        }

        // Método que indica si el vuelo ha llegado a su destino
        public bool EstaDestino()
        {
            // Usar distancia en vez de comparar referencias
            return currentPosition.Distancia(finalPosition) < 1e-6;
        }

        // Método público HasArrived
        public bool HasArrived()
        {
            return EstaDestino();
        }

        // Método para calcular la distancia a otro plan de vuelo
        public double Distance(FlightPlan plan)
        {
            return this.currentPosition.Distancia(plan.currentPosition);
        }

        // Detecta conflicto cuando los vuelos están más cerca de la distancia de seguridad
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            bool conclicto = false;

            if (this.currentPosition.Distancia(b.currentPosition) < distanciaSeguridad)
                conclicto = true;

            return conclicto;
        }

        public void EscribeConsola()
        // escribe en consola los datos del plan de vuelo
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", id);
            Console.WriteLine("Velocidad: {0:F2}", velocidad);
            Console.WriteLine("Posición actual: ({0:F2}, {1:F2})", currentPosition.GetX(), currentPosition.GetY());
            if (this.EstaDestino())
                Console.WriteLine("Ha llegado al destino");
            Console.WriteLine("******************************");
        }
    }
}
