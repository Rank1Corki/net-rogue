using net_rogue;

class Program
{
    static void Main(string[] args)
    {
        bool playAgain = true;
        string mapFilePath = "Maps/mapfile.json"; // Provide the path to the map file

        while (playAgain)
        {
            Game game = new Game();
            game.Run(mapFilePath);

            Console.WriteLine("Play again? Y/N");
            string input = Console.ReadLine();
            playAgain = (input != null && input.Trim().ToUpper() == "Y");
        }
    }
}
