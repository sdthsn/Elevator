using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
   public class Elevator
    {
        public ElevatorStatus CurrentStatus { get; private set; } = ElevatorStatus.STOPPED;
        public int CurrentFloor { get; private set; } = 1;
        public string ElevatorName { get; }
        public enum ElevatorStatus
        {
            UP,
            STOPPED,
            DOWN
        }

        //Declaration
        private bool[] destinationFloor;
        private readonly int topfloor;

        //Initiallization of an Elevator
        public Elevator(string elevator)
        {
            destinationFloor = new bool[ElevatorController.Floor + 1];
            topfloor = ElevatorController.Floor;
            this.ElevatorName = elevator;
        }

        
        private void Descend()
        {
            CurrentStatus = ElevatorStatus.DOWN;
            int floor = 0;
            //Setting stoppage while going down to take the caller on different floor
            for (int i = 0; i <= topfloor; i++)
            {
                if (ElevatorController.StoppageGoingDown[i])
                {
                    //optimizing the starting floor while going down
                    if (i > floor) floor = i;
                    destinationFloor[i] = true;
                }
            }
            //Going to downward destination to load and unload
            for (int i = floor; i >= 1; i--)
            {
                if (destinationFloor[i])
                {
                    Stop(i);
                }
                else
                    continue;
            }

            CurrentStatus = ElevatorStatus.STOPPED;
        }
        private void Ascend()
        {
            CurrentStatus = ElevatorStatus.UP;
            int floor = topfloor + 1;
            //Setting stoppage while going down to take the caller on different floor
            for (int i = 0; i <= topfloor; i++)
            {

                if (ElevatorController.StoppageGoingUp[i])
                {
                    //optimizing the starting floor while going up
                    if (i < floor) floor = i;
                    destinationFloor[i] = true;
                }

            }
            //Going to upward destination to load and unload
            for (int i = floor; i <= topfloor; i++)
            {
                if (destinationFloor[i])
                {
                    //currentFloor = i;
                    Stop(i, true);
                }
                else
                    continue;
            }

            CurrentStatus = ElevatorStatus.STOPPED;
            Descend();
        }
        private void Stop(int floor, bool goingUp = false)
        {
            CurrentStatus = ElevatorStatus.STOPPED;
            CurrentFloor = floor;
            Console.WriteLine("{0} stopped at floor {1}", ElevatorName, floor);
            //Action while door is open
            bool openDoorOperationCompleted = OpenDoor();

            if (goingUp && ElevatorController.StoppageGoingUp[floor])
            {
                SelectFloorNumber();
                ElevatorController.StoppageGoingUp[floor] = false;
            }
            if (!goingUp && ElevatorController.StoppageGoingDown[floor])
            {
                SelectFloorNumber();
                ElevatorController.StoppageGoingDown[floor] = false;
            }

            destinationFloor[floor] = false;
            //Door is closing now
           CloseDoor();

            void SelectFloorNumber()
            {
                Console.Write("Please press which floor you would like to go to : ");
                var floorInput = Console.ReadLine();
                int selectedFloor;
                //TOdo: Need to implement exception handling though in real life there is no way to enter invalid floor no
                if (Int32.TryParse(floorInput, out selectedFloor))
                {
                    if (selectedFloor > topfloor)
                    {
                        Console.WriteLine("We only have {0} floors", topfloor);
                        return;
                    }
                    else
                    {
                        if (CurrentFloor == selectedFloor)
                            StayPut();
                        else
                            destinationFloor[selectedFloor] = true;
                    }
                }
                else
                    Console.WriteLine("You have pressed an incorrect floor, Please try again");
            }
        }
        private void StayPut()
        {
            Console.WriteLine("That's our current floor");
        }
        private bool OpenDoor()
        {
            Console.WriteLine("{0} door is open now ....", ElevatorName);
            var doorOpenedAt = DateTime.Now;
            bool doorOpened = true;
            //Prarrel run of two method to check how long door is opened forcely and beep
            Parallel.Invoke(() => ForceKeppingDoorOpen(), () => BlowBeep(doorOpenedAt));
            void ForceKeppingDoorOpen()
            {
                Console.WriteLine("\nPress '<' key to keep the {0} door open, press any other key to continue regular movement: ", ElevatorName);
                //var buttorPress = Console.ReadLine();
                bool moreTime = true;
                while (moreTime)
                {
                    var buttorPress = Console.ReadLine();
                    if (string.Equals(buttorPress, "<"))
                    {
                        Thread.Sleep(20000);
                        Console.WriteLine("\nPress '<' key to keep the {0} door open, press any other key to continue regular movement: ", ElevatorName);
                    }
                    else
                    {
                        doorOpened = false;
                        moreTime = false;
                    }
                }
            }
            void BlowBeep(DateTime doorOpeningTime)
            {
                while (doorOpened)
                {
                    if (DateTime.Now > doorOpenedAt.AddSeconds(60))
                    {
                        Console.WriteLine("Beep ..Beep..Beep");
                        Console.Beep(1000, 1000);
                        Console.WriteLine("Close stop forcing to keep the {0} door open, People are waiting......",ElevatorName);
                        Console.Beep(1000, 1000);
                        Console.Beep(1000, 1000);
                        //break;
                    }

                }

            }

            return true;
        }
        private void CloseDoor()
        {
            Console.WriteLine("The door of {0} is closing....", ElevatorName);
        }

        public void ElevatorStart()
        {
            if (ElevatorController.SwitchElevator)
            {
                Ascend();
                while (ElevatorController.StoppageGoingUp.Where(c => c).Count() > 0 || ElevatorController.StoppageGoingDown.Where(c => c).Count() > 0)
                {
                    Descend();
                    ElevatorController.SwitchElevator = false;
                }
            }
            else
            {
                Descend();
                while (ElevatorController.StoppageGoingUp.Where(c => c).Count() > 0 || ElevatorController.StoppageGoingDown.Where(c => c).Count() > 0)
                {
                    Ascend();
                }

            }

        }
    }
}
