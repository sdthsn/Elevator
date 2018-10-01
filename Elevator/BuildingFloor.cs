using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public static class BuildingFloor
    {
        public static void ElevatorCaller()
        {
            const string EXIT = "e";
            string input = string.Empty;
            string direction = string.Empty;
            //int elevators, floor; string elevatorInput, floorInput;
            while (input != EXIT)
            {
                Console.Write("Please press the direction you want to go, 'U' for UP, any other key for down or press e to stp the input: ");
                direction = Console.ReadLine();
                Console.Write("Enter the floor you standing : ");
                input = Console.ReadLine();
                if (Int32.TryParse(input, out int floor))
                    ElevatorController.CallButtonPress(floor, direction);
                else if (input == EXIT)
                    Console.WriteLine("GoodBye!");
                else
                    Console.WriteLine("You have pressed an incorrect floor, Please try again");
            }
        }
    }
}
