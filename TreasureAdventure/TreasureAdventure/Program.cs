using System;

namespace TreasureAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            int? tempRoomID=null;
            
            IMazeImplementation instance = new IMazeImplementation();
            instance.BuildMaze(3); //3x3 maze assumed
            Console.WriteLine("-------------The Treasure Adventure Emulator---------------");
            Console.WriteLine("Basic Control Keys:");
            Console.WriteLine("North : N");
            Console.WriteLine("South : S");
            Console.WriteLine("East  : E");
            Console.WriteLine("West  : W");
            Console.WriteLine("Exit  : Esc");
            Console.WriteLine("Maze Initialized...");
            int RoomID = instance.GetEntranceRoom();

            Console.WriteLine("Welcome to the Entrance!");

            int keycount = 0;
            int health = 3;

            while (health>0)
            {
                Console.WriteLine(instance.GetDescription(RoomID));
                Console.WriteLine("Total Moves: "+ keycount);
                key = Console.ReadKey();
                Console.WriteLine();
                keycount++;
                if(key.Key==ConsoleKey.Escape)
                {
                    break;
                }
                if(key.KeyChar == 'N')
                {
                    tempRoomID = instance.GetRoom(RoomID, 'N');
                    if (tempRoomID != null)
                    RoomID = Convert.ToInt32(tempRoomID);
                }else if (key.KeyChar == 'S')
                {
                    tempRoomID = instance.GetRoom(RoomID, 'S');
                    if (tempRoomID != null)
                        RoomID = Convert.ToInt32(tempRoomID);
                }
                else if (key.KeyChar == 'E')
                {
                    tempRoomID = instance.GetRoom(RoomID, 'E');
                    if (tempRoomID != null)
                        RoomID = Convert.ToInt32(tempRoomID);
                }
                else if (key.KeyChar == 'W')
                {
                    tempRoomID = instance.GetRoom(RoomID, 'W');
                    if (tempRoomID != null)
                        RoomID = Convert.ToInt32(tempRoomID);
                }
                else
                {
                    Console.WriteLine("Invalid Key!");
                }

                if (tempRoomID == null)
                {
                    Console.WriteLine("Lookout!! Maze Wall!");
                }
                else
                {
                    if (instance.HasTreasure(RoomID))
                    {
                        Console.WriteLine("Congratulations!! Treasure Found!!");
                        Console.WriteLine("Total Moves: " + keycount);
                        Console.ReadKey();
                        break;
                    }

                    if (instance.CausesInjury(RoomID))
                    {
                        Console.WriteLine("Trap Room!! Injury!!");
                        health--;
                    }
                }
            }

            if (health == 0)
            {
                Console.WriteLine("Good Day to Die Hard!! Game Over!!");
                Console.ReadKey();
            }
        }
    }
}
