using rogue8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;


namespace net_rogue
{
    internal enum Race
    {
        Human,
        Elf,
        Orc
    }

    internal enum Class
    {
        Warrior,
        Mage,
        Gunner
    }
    internal class PlayerCharacter
    {
        private Game game;

        public string name;
        public Race rotu;
        public Class hahmoluokka;

        public Point2D position;

        private char image;
        private Color color;
        private Map map;

        public Texture raylibimage;
        public int imagePixelX;
        public int imagePixelY;

        public PlayerCharacter(char image, Color color, Map map)
        {
            this.image = image;
            this.color = color;
            this.map = map;
            this.position = new Point2D(2, 2); // Initialize player position
        }

        public void Move(int x_move, int y_move, Map map)
        {
            int newX = position.x + x_move;
            int newY = position.y + y_move;

            int tile = map.getTile(newX, newY);

            if (tile == 1)
            {
                // Only move if the tile is walkable (tileId == 1)
                position.x += x_move;
                position.y += y_move;
                // Keep the unit inside the map boundaries
                position.x = Math.Clamp(position.x, 0, map.mapWidth - 1);
                position.y = Math.Clamp(position.y, 0, map.mapTiles.Length / map.mapWidth - 1);
            }
        }

        public void Draw(Texture atlasImage, int imagesPerRow, int index)
        {
            int mapWidth = map.mapWidth;
            int totalTiles = map.mapTiles.Length;


            // Draw the player
            Raylib.DrawRectangle(position.x * Game.tileSize, position.y * Game.tileSize, Game.tileSize, Game.tileSize, color);
            Raylib.DrawText(image.ToString(), position.x * Game.tileSize + Game.tileSize / 4, position.y * Game.tileSize + Game.tileSize / 4, Game.tileSize, Raylib.WHITE);

            // Calculate the adjusted position for drawing the floor tile
            int adjustedY = (int)((position.y - 0.35f) * Game.tileSize);

            // Draw the floor tile behind the player character
            Raylib.DrawTexture(atlasImage, position.x * Game.tileSize, adjustedY, Raylib.WHITE);
        }




        // Helper method to get tile color based on tile ID
        private static Color GetTileColor(int tileId)
        {
            switch (tileId)
            {
                case 1:
                    // Floor color
                    return Raylib.WHITE;
                case 2:
                    // Wall color
                    return Raylib.GRAY;
                default:
                    // Default color for other tiles
                    return Raylib.LIGHTGRAY;
            }
        }
    }
}