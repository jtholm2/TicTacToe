using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class TicTacToe
    {
        private string userPiece;
        private string computerPiece;
        private Dictionary<int, string> board;
        private Dictionary<int[], int> userSelections;
        private Dictionary<int[], int> computerSelections;
        private double size;
        private int length;
        private List<string> historicalChoices;

        public TicTacToe(int size)
        {
            this.userPiece = "X";
            this.computerPiece = "O";
            this.historicalChoices = new List<string>();
            this.userSelections = new Dictionary<int[], int>();
            this.computerSelections = new Dictionary<int[], int>();
            this.board = new Dictionary<int, string>();
            this.size = Math.Pow(size + 1, 2);
            this.length = size + 1;
        }

        public void InitializeBoard()
        {
            int counter = 1;
            for (int i = 0; i < this.size; i++)
                if (i < this.length)
                    this.board.Add(i, i.ToString());

                else if(i % this.length == 0)
                {
                    this.board.Add(i, counter.ToString());
                    counter++;
                }
                else
                    this.board.Add(i, " ");
        }

        public void EstablishWinningBoards()
        {
            int a = 0;
            while (a < this.length - 1)//to establish rows and columns winning options
            {
                int[] rowWinningSolutions = new int[this.length - 1];
                int[] columnWinningSolutions = new int[this.length - 1];

                for (int i = 1; i < this.length; i++)
                {
                    rowWinningSolutions[i - 1] = i + (this.length * (a + 1));
                    columnWinningSolutions[i - 1] = (a+1) + (this.length * i);
                }
                this.userSelections.Add(rowWinningSolutions, 0);
                this.userSelections.Add(columnWinningSolutions, 0);
                this.computerSelections.Add(rowWinningSolutions, 0);
                this.computerSelections.Add(columnWinningSolutions, 0);
                a++;
            }

            int[] crossDiagonalTopLeft = new int[this.length-1];
            int[] crossDiagonalTopRight = new int[this.length-1];

            for(int k = 1; k < this.length; k++)//to establish the diagonal winning options
            {
                crossDiagonalTopLeft[k-1] = (this.length * k) + k;
                crossDiagonalTopRight[k-1] = (this.length * (this.length - k)) + k;
            }

            this.userSelections.Add(crossDiagonalTopLeft, 0);
            this.userSelections.Add(crossDiagonalTopRight, 0);
            this.computerSelections.Add(crossDiagonalTopLeft, 0);
            this.computerSelections.Add(crossDiagonalTopRight, 0);
        }

        public void PrintBoard()
        {
            int counter = 0;
            foreach(KeyValuePair<int, string> item in this.board)
            {
                counter++;
                if (counter % this.length == 0)
                    Console.WriteLine(item.Value);
                else
                    Console.Write(item.Value + "|");
            }

        }

        public void MakeASelection()
        {
            //user selection
            UserSelection();

            //Computer selection
            ComputerSelection();

            PrintHistoricalChoices();
        }

        public void UserSelection()
        {
            string selection = "";
            while (true)
            {
                Console.WriteLine("Please make a selection following a x,y coordinate based on the above board (ex. 11 or 32): ");
                selection = Console.ReadLine();
                if (selection.Length == 2 && (!this.historicalChoices.Contains(selection)))//checks if the input was in correct format and hasn't been selected previously
                {
                    this.historicalChoices.Add(selection);
                    break;
                }
                Console.WriteLine("Try again");
            }

            int[] userSelection = new int[2];
            int accessUserSelection = 0;
            foreach (char digit in selection)//turns the selection string into an int array
            {
                int number = (int)Char.GetNumericValue(digit);
                userSelection[accessUserSelection] = number;
                accessUserSelection++;
            }

            this.board[userSelection[0] + (this.length * userSelection[1])] = this.userPiece;//changes the board to reflect the user's selection

            List<int[]> keyList = new List<int[]>(this.userSelections.Keys.ToList());
            foreach (int[] test in keyList)//if one of the keys in the user's winning solution dictionary contains the 'coordinate' that was chosen, it'll increase the value by one
            {
                if (test.Contains(userSelection[0] + (this.length * userSelection[1])))
                    this.userSelections[test] += 1;
            }
        }

        public void ComputerSelection()
        {
            Random rand = new Random();
            int[] computerSelection = new int[2];
            while (true)
            {
                string test = "";
                computerSelection[0] = rand.Next(1, this.length);
                computerSelection[1] = rand.Next(1, this.length);
                foreach (int digit in computerSelection)
                    test += digit.ToString();

                if (!this.historicalChoices.Contains(test))
                {
                    this.historicalChoices.Add(test);
                    break;
                }

            }
            //add comp selection to the board, then to the list of the computer's selections 
            this.board[computerSelection[0] + (this.length * computerSelection[1])] = this.computerPiece;
            List<int[]> computerTest = new List<int[]>(this.computerSelections.Keys.ToList());
            foreach (int[] test in computerTest)
            {
                if (test.Contains(computerSelection[0] + (this.length * computerSelection[1])))
                    this.computerSelections[test] += 1;
            }
        }

        public void PrintHistoricalChoices()
        {
            Console.WriteLine("The historical choices are: ");
            foreach (string select in this.historicalChoices)
            {
                Console.Write(select + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public bool DidSomeoneWin()
        {
            bool answer = false;
            if (DidUserWin())
            {
                Console.WriteLine("Congrats, you won!");
                answer = true;
            }
            else if (DidComputerWin())
            {
                Console.WriteLine("Aww, better luck next time!");
                answer = true;
            }
            return answer;
                
        }
        public bool DidUserWin()
        {
            bool answer = false;
            foreach(KeyValuePair<int[], int> item in this.userSelections)
            {
                if (item.Value == this.length - 1)
                    answer = true;
            }
            return answer;
        }

        public bool DidComputerWin()
        {
            bool answer = false;
            foreach (KeyValuePair<int[], int> item in this.computerSelections)
            {
                if (item.Value == this.length - 1)
                    answer = true;
            }
            return answer;
        }
    }
}
