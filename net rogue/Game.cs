using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace net_rogue
{
    internal class Game
    {
        
        public string AskName()
        {
            
            while (true)
            {
                Console.Write("Anna nimi: ");
                string nimi = Console.ReadLine();
                if (String.IsNullOrEmpty(nimi))
                {
                    Console.WriteLine("Ei kelpaa");
                    continue;
                }
                bool nameOk = true;
                for (int i = 0; i < nimi.Length; i++)
                {
                    char kirjain = nimi[i];
                    if (char.IsLetter(kirjain) == false)
                    {
                        nameOk = false; 
                        break;
                    }
                }
                if (nameOk)
                {
                    return nimi;
                }
            }
        }

        public void AskRace()
        {
            PlayerCharacter player = new PlayerCharacter();
            Console.WriteLine("Select race for your character");
            // Get the names of enums as array
            string[] raceNames = Enum.GetNames(typeof(Race));
            // Write all the names
            for (int i = 0; i < raceNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {raceNames[i]}");
            }
            string raceAnswer = Console.ReadLine();
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
            player.position = new Point2D(10, 10);

            // Init and run game loop until ESC is pressed
            Console.Clear();
            player.Draw();

            bool game_running = true;
            while (game_running)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        player.Move(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        player.Move(0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        player.Move(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        player.Move(1, 0);
                        break;

                    case ConsoleKey.Escape:
                        game_running = false;
                        break;

                    default:
                        break;
                };
                Console.Clear();
                player.Draw();
            }

        }
    }

}
