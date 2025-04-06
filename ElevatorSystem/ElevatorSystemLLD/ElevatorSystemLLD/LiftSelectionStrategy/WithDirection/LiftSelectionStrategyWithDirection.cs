using ElevatorSystemLLD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystemLLD.LiftSelectionStrategy.WithDirection
{
    public abstract class LiftSelectionStrategyWithDirection
    {
        protected ElevatorController elevatorController;
        public LiftSelectionStrategyWithDirection(ElevatorController elevatorController)
        {
            this.elevatorController = elevatorController;
        }
        public abstract int SelectElevator(int sourceFloorId, Direction direction);
    }
}
