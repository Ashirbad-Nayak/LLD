using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystemLLD.LiftSelectionStrategy.WithDestination
{
    public abstract class LiftSelectionStrategyWithDestination
    {
        protected ElevatorController elevatorController;
        public LiftSelectionStrategyWithDestination(ElevatorController elevatorController)
        {
            this.elevatorController = elevatorController;
        }

        public abstract int SelectElevator(int SourceFloorId, int DestinationFloorId);
    }
}
