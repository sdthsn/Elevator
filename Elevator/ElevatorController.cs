using System;
namespace Elevator
{
    // This class is used to controll the Call from users from different floors and keep the requests Global and handle it by n number of elevator globally
    public static class ElevatorController
    {
        //public static int Floor { get; set; }
        //public static  bool[] StoppageGoingUp { get; set; }
        //public static bool[] StoppageGoingDown { get; set; }
        //public static bool SwitchElevator { get; set; }
        public volatile static int Floor;
        public volatile static bool[] StoppageGoingUp;
        public static volatile bool[] StoppageGoingDown;
        public static volatile bool SwitchElevator;


        public static void  CreateStopages(int floor)
        {
            Floor = floor; 
            StoppageGoingDown = new bool[floor + 1];
            StoppageGoingUp = new bool[floor + 1];
            SwitchElevator = true;
        }

        public static void CallButtonPress(int floor, string direction)
        {
            if (string.Equals(direction, "U"))
            {
                StoppageGoingUp[floor] = true;
            }
            else StoppageGoingDown[floor] = true;
        }



    }
}
