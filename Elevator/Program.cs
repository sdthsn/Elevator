using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
    #region Read me
    /**
     ##### Assignment ########
     Write an algorithm to control multiple elevators in an apartment building. The algorithm should be able to handle N floors (minimum 5) in a building and should have a minimum of 2 elevators. If your algorithm requires a master data structure then implement that as well (don’t use built in data structure). Algorithm should handle following operational scenarios.

        1. Efficient Scheduling of elevator

        2. Up/Down direction so elevator goes to appropriate floor

        3. Door open/close (don’t move the elevator if door is open)

        4. Sound an alarm if the elevator door stays open for longer than 60 seconds.
   #############################################################################################
        
       #1 and #2 requirement is implemented throughout the project
        #3 and #4 requirements is implemented on Elevator Class in line 145 to 195

        No built-in data structer like: Queuw, Stack, Dickonary, Sortedlist ArrayList used as of requirement. Only array is used to operate the n elevator.
         */

    #endregion
    class Program
    {
        /*
         Some advanced feature of C# 7.0 is used, So you need C# 7.0 to run this app
         I mean i used some nested method which is a advanced feature of C# 7.0)
         */
        public static void Main(string[] args)
        {
            Console.Write("Your elevator is starting now");
            Building building = Init();

            //Starting the Elevator Operation
            ElevatorControlSystem control = new ElevatorControlSystem(building);

            Console.ReadKey();
        }

        #region Helper methods
        private static Building Init()
        {
            Start:

                Building building = new Building();
                Console.Write("Enter the total number of floors in the Building: ");
                string floorInput = Console.ReadLine();


                if (int.TryParse(floorInput, out int floor))
                {
                    building.Floors = floor;
                }
                else
                {
                    StartOver();
                    goto Start;
                }
                Console.Write("Enter the total number of elevator(s) in the Building: ");
                string elevatorInput = Console.ReadLine();
                if (int.TryParse(elevatorInput, out int elevators))
                {
                    building.Elevators = elevators;
                }
                else
                {
                    StartOver();
                    goto Start;
                }
                return building;
        }
        static void StartOver()
        {
            Console.WriteLine("Wrong entry...");
            Console.Beep();
            Thread.Sleep(2000);
            Console.Clear();  
        }
        #endregion

    }
}

