using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
    public class ElevatorControlSystem
    {
        public ElevatorControlSystem(Building building)
        {
            //Initiallizing the Elevator Controller
            ElevatorController.CreateStopages(building.Floors);
            //Start Movinf Elevator
            OperateElevators(building.Floors,building.Elevators);

        }
        private void OperateElevators(int floor, int elevatorsNumber)
        {
            BuildingFloor.ElevatorCaller();
            // Creating n intance for Elevators
            var elevators = Elevators(elevatorsNumber);
            //Operating ElevatorController and Multiple elevator paralleli
            var threads = new List<Thread>
            {
                new Thread(BuildingFloor.ElevatorCaller)
            };
            elevators.ForEach(x => threads.Add(new Thread(x.ElevatorStart)));
            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());
        }
        private void DoWork(int floor)
        {
            //Initializing the Floor and destination
            ElevatorController.CreateStopages(floor);
            Elevator elevator1 = new Elevator("Elevator1");
            Elevator elevator2 = new Elevator("Elevator2");
            BuildingFloor.ElevatorCaller();
            Parallel.Invoke(() => elevator1.ElevatorStart(), () => elevator2.ElevatorStart(), () => BuildingFloor.ElevatorCaller());

        }
      

        private List<Elevator> Elevators(int elevators)
        {
            List<Elevator> elevatorsInfo = new List<Elevator>();
            for (int i = 1; i <= elevators; i++)
            {
                elevatorsInfo.Add(new Elevator(string.Concat("Elevator ", i.ToString())));
            }
            return elevatorsInfo;
        }

    

    }
}
