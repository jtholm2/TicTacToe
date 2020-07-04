using System;

namespace TicTacToe
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter a size greater than 3 and less than 10: ");
            int size = Convert.ToInt32(Console.ReadLine());
            UpdatedGame game = new UpdatedGame(size);
            game.InitializeBoard();
            game.EstablishWinningBoards();
            while (true)
            {
                if (!game.DidSomeoneWin())
                {
                    game.PrintBoard();
                    game.MakeASelection();
                }
                else
                {
                    break;
                }

            }

        }
    }
}
