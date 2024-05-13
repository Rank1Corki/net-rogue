using rogue8;
using System;
using System.Numerics;
using System.Text.RegularExpressions;
using ZeroElectric.Vinculum;

namespace net_rogue
{
    internal class Game
    {
        PlayerCharacter character;
        Map level01;
        Map map;
        public Texture tilemap;
        public Texture tilemap1;
        public Texture player1;

        public static readonly int tileSize = 16;

        public void SetImageAndIndex(Texture atlasImage, int imagesPerRow, int index)
        {
            // Call the Draw method in the PlayerCharacter class and pass atlasImage, imagesPerRow, and index as arguments
            character.Draw(atlasImage, imagesPerRow, index);
        }
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
            PlayerCharacter player = new PlayerCharacter('!', Raylib.RED, map);
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

        public void Init()
        {
            Raylib.InitWindow(480, 270, "Rogue");
            Raylib.SetTargetFPS(30);
            tilemap = Raylib.LoadTexture("Tilemaps/Wall.png");
            tilemap1 = Raylib.LoadTexture("Tilemaps/Floor.png");
            player1 = Raylib.LoadTexture("Tilemaps/doc_idle_anim_f0.png");
        }


        // Add the LoadMap method to load the map
        private void LoadMap(string mapFilePath)
        {
            MapLoader loader = new MapLoader();
            map = loader.LoadMapFromFile(mapFilePath);
        }





        // Add the Run method to start the game loop
        public void Run(string mapFilePath)
        {
            string playerName = AskName();
            Race playerRace = AskRace();

            Init(); // Initialize the game window and target FPS
            LoadMap(mapFilePath); // Load the map from file


            // Initialize the player character
            PlayerCharacter player = new PlayerCharacter('.', Raylib.WHITE, map);
            player.name = playerName;
            player.rotu = playerRace;
            player.position = new Point2D(1, 1);
            Console.Clear();
            bool gameRunning = true;
            while (!Raylib.WindowShouldClose() && gameRunning)
            {
                // Handle player input and movement
                if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                    player.Move(0, -1, map);
                else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
                    player.Move(0, 1, map);
                else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                    player.Move(-1, 0, map);
                else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                    player.Move(1, 0, map);

                // Draw the game
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.WHITE);
                map.Draw(tilemap, tilemap1, tileSize);
                player.Draw(player1, 5, 5);
                Raylib.EndDrawing();

                // Check for game exit
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
                    gameRunning = false;
            }

            Raylib.CloseWindow();
        }

    }
}
