using ZeroElectric.Vinculum;

namespace rogue8
{
    internal class Map
    {
        public int mapWidth;
        public int[] mapTiles;

        public int getTile(int x, int y)
        {
            int index = x + y * mapWidth;
            int tileId = mapTiles[index];
            return tileId;
        }

        public void Draw(Texture floorTexture, Texture wallTexture, int tileSize)
        {
            int mapHeight = mapTiles.Length / mapWidth;

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int index = x + y * mapWidth;
                    int tileId = mapTiles[index];

                    int posX = (int)(x * tileSize);
                    int posY = (int)(y * tileSize);

                    // Draw the appropriate texture based on tile ID
                    if (tileId == 1)
                    {
                        // Draw floor texture
                        Raylib.DrawTexture(floorTexture, posX, posY, Raylib.WHITE);
                    }
                    else if (tileId == 2)
                    {
                        // Draw wall texture
                        Raylib.DrawTexture(wallTexture, posX, posY, Raylib.WHITE);
                    }
                    // Add more conditions for other tile IDs if needed
                }
            }
        }
    }
}
