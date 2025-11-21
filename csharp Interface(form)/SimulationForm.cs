        // Devuelve la lista de pares en conflicto actual (distancia instantánea)
        private List<Tuple<FlightPlan, FlightPlan>> GetConflictingPairs()
        {
            List<Tuple<FlightPlan, FlightPlan>> result = new List<Tuple<FlightPlan, FlightPlan>>();
            int n = _flightPlans.getnum();
            int i = 0;
            while (i < n)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                int j = i + 1;
                while (j < n)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);
                    if (a != null && b != null && a.Conflicto(b, _securityDistance))
                    {
                        result.Add(Tuple.Create(a, b));
                    }
                    j++;
                }
                i++;
            }
            return result;
        }

        private void cyclebtn_Click(object sender, EventArgs e)
        {
            SaveCurrentState();

            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);
            }
            UpdateFlightsUI();

            var conflictos = GetConflictingPairs();
            if (conflictos.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Conflicto detectado. Se detiene la simulación.");
                foreach (var par in conflictos)
                {
                    sb.Append("- ").Append(par.Item1.GetId())
                      .Append(" con ").Append(par.Item2.GetId()).AppendLine();
                }
                MessageBox.Show(sb.ToString(), "Conflicto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                simulationTimer.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SaveCurrentState();

            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);
            }
            UpdateFlightsUI();

            var conflictos = GetConflictingPairs();
            if (conflictos.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Conflicto detectado. Se detiene la simulación.");
                foreach (var par in conflictos)
                {
                    sb.Append("- ").Append(par.Item1.GetId())
                      .Append(" con ").Append(par.Item2.GetId()).AppendLine();
                }
                MessageBox.Show(sb.ToString(), "Conflicto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                simulationTimer.Stop();
            }
        }