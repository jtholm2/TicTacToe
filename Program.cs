using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class Game
    {
        private string[,] board;
        private string userPiece;
        private string computerPiece;
        private Dictionary<string, int[]> choices;

        public Game(string user, string computer)
        {
            this.board = new string[3, 3];
            this.userPiece = user;
            this.computerPiece = computer;
            this.choices = new Dictionary<string, int[]>() {
                                                                {"UL", new[]{0,0}},
                                                                {"UM", new[]{0,1}},
                                                                {"UR", new[]{0,2}},
                                                                {"ML", new[]{1,0}},
                                                                {"MM", new[]{1,1}},
                                                                {"MR", new[]{1,2}},
                                                                {"LL", new[]{2,0}},
                                                                {"LM", new[]{2,1}},
                                                                {"LR", new[]{2,2}}
                                                            };
        }

        public void initializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.board[i, j] = " ";
                }
            }
        }

        public void printBoard()
        {
            int counter = 0;
            foreach (string a in board)
            {
                counter++;
                if (counter % 3 == 0)
                    Console.WriteLine(a);
                else
                    Console.Write(a + "|");
            }
        }

        public bool DidSomeoneWin()
        {
            int i = 0;
            int j = 0;
            if(this.board[i,j] != " " && this.board[i,j] == this.board[i,j+1] && this.board[i,j] == this.board[i, j + 2])
            {
                if (this.board[i, j].Equals(this.userPiece))
                    Console.WriteLine("Congrats! You won!");
                else
                    Console.WriteLine("Sorry, better luck next time!");
                return true;

            }
            else if(this.board[i, j] != " " && this.board[i,j] == this.board[i+1,j] && this.board[i, j] == this.board[i+2, j])
            {
                if (this.board[i, j].Equals(this.userPiece)) 
                    Console.WriteLine("Congrats! You won!");
                else
                    Console.WriteLine("Sorry, better luck next time!");
                return true;
            }
                else if(this.board[i, j] != " " && this.board[i,j] ==  this.board[i+1,j+1] && this.board[i,j] == this.board[i+2,j+2])
            {
                if(this.board[i, j].Equals(this.userPiece))
                    Console.WriteLine("Congrats! You won!");
                else
                    Console.WriteLine("Sorry, better luck next time!");
                return true;
            }
            else if(this.board[i, j+2] != " " && this.board[i,j+2] == this.board[i+1,j+1] && this.board[i,j+2] == this.board[i + 2, j])
            {
                if(this.board[i, j+2].Equals(this.userPiece))
                    Console.WriteLine("Congrats! You won!");
                else
                    Console.WriteLine("Sorry, better luck next time!");
                return true;
            }
            return false;
        }
        public void MakeASelection()
        {
            Console.WriteLine("Please make a selection from the following:\nUL, UM, UR, ML, MM, MR, LL, LM, LR");
            string selection = Console.ReadLine().ToUpper();
            foreach(KeyValuePair<string, int[]> entry in this.choices)
            {
                if (entry.Key.Equals(selection))
                {
                    this.board[entry.Value[0],entry.Value[1]] = this.userPiece;
                    this.choices.Remove(entry.Key);
                    break;
                }
            }

            Random rand = new Random();
            string[] newChoices = this.choices.Keys.ToArray();
            string randomChoice = newChoices[rand.Next(0, newChoices.Length)];
            
            foreach (KeyValuePair<string, int[]> entry in this.choices)
            {
                if (entry.Key.Equals(randomChoice))
                {
                    this.board[entry.Value[0], entry.Value[1]] = this.computerPiece;
                    this.choices.Remove(entry.Key);
                    break;
                }
            }
        }

        public void startGame()
        {
            Console.WriteLine("Would you like to play tic-tac-toe? Enter y or n: ");
            string userChoice = Console.ReadLine().ToLower();
            initializeBoard();
            if (userChoice.Equals("y"))
            {
                while (true)
                {
                    printBoard();
                    if (DidSomeoneWin())
                    {
                        break;
                    }
                    else
                        MakeASelection();
                }
            }
            Console.WriteLine("Thanks for playing!");
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            Game test = new Game("X", "O");
            test.startGame();
        }
    }
}
