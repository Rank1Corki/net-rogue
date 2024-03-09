using rogue8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace net_rogue
{
    internal class Game
    {

        Map level01;
        Map map;
        MapLoader loader;
        public string AskName()
        {
            while (true)
            {
                Console.Write("Enter your name (letters only): ");

                // Read user input
                string name = Console.ReadLine();

                // Check if the input is empty
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    continue;
                }

                // Use Regex to check if the input contains only letters
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    Console.WriteLine("Name can only contain letters.");
                    continue;
                }

                // Input is valid, return the name
                return name;
            }
        }

        public Race AskRace()
        {
            PlayerCharacter player = new PlayerCharacter('!', ConsoleColor.Red);
            Console.WriteLine("Select race for your character");

            // Get the names of enums as array
            string[] raceNames = Enum.GetNames(typeof(Race));

            // Write all the names
            for (int i = 0; i < raceNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {raceNames[i]}");
            }

            // Get the user's input
            int raceIndex;
            while (true)
            {
                Console.Write("Enter the number corresponding to your race: ");
                string raceInput = Console.ReadLine();

                // Validate the input
                if (int.TryParse(raceInput, out raceIndex) && raceIndex >= 1 && raceIndex <= raceNames.Length)
                {
                    break; // Input is valid
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid race number.");
                }
            }

            // Convert the input into the corresponding Race enum value
            return (Race)(raceIndex - 1); // Adjust index to match enum starting from 0
        }


        public void Run()
        {
            // Prepare to show game
            Console.CursorVisible = false;

            // A small window
            Console.WindowWidth = 60;
            Console.WindowHeight = 26;

            
            // Create player
            PlayerCharacter player = new PlayerCharacter('@', ConsoleColor.Green);
            player.name = AskName();
            player.rotu = AskRace();
            player.position = new Point2D(1, 1);

            // Init and run game loop until ESC iqs pressed
            Console.Clear();
            MapLoader loader = new MapLoader();
            level01 = loader.LoadMapFromFile("Maps/mapfile.json");
            level01.Draw();
            player.Draw();

            bool game_running = true;
            while (game_running)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        player.Move(0, -1, level01);
                        break;
                    case ConsoleKey.DownArrow:
                        player.Move(0, 1, level01);
                        break;
                    case ConsoleKey.LeftArrow:
                        player.Move(-1, 0, level01);
                        break;
                    case ConsoleKey.RightArrow:
                        player.Move(1, 0, level01);
                        break;

                    case ConsoleKey.Escape:
                        game_running = false;
                        break;

                    default:
                        break;
                };


                Console.Clear();
                level01.Draw();
                player.Draw();
            }

        }
    }

}
