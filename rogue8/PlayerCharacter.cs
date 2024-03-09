using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection;
using System;
using rogue8;


namespace net_rogue
{
    enum Race
    {
        Human,
        Elf,
        Orc
    }
    enum Class
    {
        Warrior,
        Mage,
        Gunner
    }
    internal class PlayerCharacter
    {
        public string name;
        public Race rotu;
        public Class hahmoluokka;

        public Point2D position;

        private char image;
        private ConsoleColor color;

        public PlayerCharacter(char image, ConsoleColor color)
        {
            this.image = image;
            this.color = color;
        }
        
        public void Move(int x_move, int y_move, Map map)
        {
            int newX = position.x + x_move;
            int newY = position.y + y_move;

            int tile = map.getTile(newX, newY);
            
            if (tile == 1)
            {
                position.x += x_move;
                position.y += y_move;
                // This keeps the unit inside Console window
                position.x = Math.Clamp(position.x, 0, Console.WindowWidth - 1);
                position.y = Math.Clamp(position.y, 0, Console.WindowHeight - 1);
            }
            
        }

        public void Draw()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(image);
        }
    }
}
