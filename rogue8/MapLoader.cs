using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rogue8
{
    internal class MapLoader
    {

        public Map LoadTestMap()
        {
            Map map = new Map();
            map.mapWidth = 8;
            map.mapTiles = new int[] {
            2, 2, 2, 2, 2, 2, 2, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 2, 2,
            2, 2, 2, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 1, 2,
            2, 2, 2, 2, 2, 2, 2, 2 };
            return map;
        }

        public Map LoadMapFromFile(string filename)
        {
            bool fileFound = File.Exists(filename);
            if (!fileFound)
            {
                Console.WriteLine($"File {filename} not found");
                return LoadTestMap(); // Return the test map as fallback
            }

            string fileContents;

            using (StreamReader reader = File.OpenText(filename))
            {
                fileContents = reader.ReadToEnd(); // Read all lines into fileContents
            }

            Map loadedMap = JsonConvert.DeserializeObject<Map>(fileContents); // Deserialize JSON into Map object

            return loadedMap;
        }
    }
}
