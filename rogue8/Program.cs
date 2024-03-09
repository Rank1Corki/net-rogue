using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_rogue
{
    internal class Program
    {
        public static void Main()
        {
            bool again = true;
            while (again)
            {
                Game rogue = new Game();
                rogue.Run();

                Console.WriteLine("Play again? Y/N");
                if (Console.ReadLine() == "N")
                {
                    again = false;
                }
            }
        }
    }
}
