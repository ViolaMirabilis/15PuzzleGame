using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _15puzzle
{
    public class Board
    {
        public int Rows { get; private set; } = 4;
        public int Columns { get; private set; } = 4;
        public bool IsPlaying { get; set; } = false;
        public int[,] PuzzleBoard { get; set; }

        // for custom size
        // default constructor
        public Board()
        {
            PuzzleBoard = new int[Rows, Columns];       // could also initialise a board in the GenerateShuffledBoard. Gotta decide.
            PuzzleBoard = GenerateShuffledBoard();
        }

        public void DisplayBoard()
        {
            for( int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    // formatting purposes
                    if (PuzzleBoard[i,j] == 0)
                    {
                        Console.Write($"    ");        // draws empty space for visuals (0 is the placeholder of an unknown value)
                    }
                    else if (PuzzleBoard[i,j] < 10)
                    {
                        Console.Write($"{PuzzleBoard[i, j]}   ");
                    }
                    else
                    {
                        Console.Write($"{PuzzleBoard[i, j]}  ");
                    }
                        
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        // randomised
        public int[,] GenerateShuffledBoard()
        {
            BoardShuffle generator = new BoardShuffle();
            int[] randomisedNumbers = generator.Shuffle();
            int numberIndex = 0;
            for(int i = 0; i < Rows; i++)
            {

                for (int j = 0; j < Columns; j++)
                {
                    PuzzleBoard[i, j] = randomisedNumbers[numberIndex];
                    numberIndex++;
                }
            }

            return PuzzleBoard;

        }

        // checks 4 location. If one of them returns true - the number is movable
        public bool IsMovable((int, int) number)
        {
            int r = number.Item1;
            int c = number.Item2;

            if (c > 0 && PuzzleBoard[r, c - 1] == 0) return true;       // neighbour on top
            if (c < 3 && PuzzleBoard[r, c + 1] == 0) return true;       // neighbour on the bottom
            if (r > 0 && PuzzleBoard[r - 1, c] == 0) return true;       // neighbour on the left
            if (r < 0 && PuzzleBoard[r + 1, c] == 0) return true;       // neighbour on the right

            return false;
        }

        // if can move up indicates that the cell ABOVE is empty
        public (int, int)? MoveUpCoordinates()
        {
            // checks 3 rows (excluding the first one), to check for zeroes on the first one
            for(int i = 1; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    if (PuzzleBoard[i-1, j] == 0)       // checks if the first row is 0 (so a tile can be moved there)
                        return (i, j);
                }
            }
            return null;
        }

        public (int, int)? MoveDownCoordinates()
        {
            // checks first 3 rows and checks for 0 on the 4th
            for (int i = 0; i < Rows - 1; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (PuzzleBoard[i + 1, j] == 0)
                        return (i, j);
                }
            }
            return null;
        }

        public (int, int)? MoveLeftCoordinates()
        {
            // checks first 3 rows and checks for 0 on the 4th
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 1; j < Columns; j++)
                {
                    if (PuzzleBoard[i, j-1] == 0)
                        return (i, j);
                }
            }
            return null;
        }

        public (int, int)? MoveRightCoordinates()
        {
            // checks first 3 rows and checks for 0 on the 4th
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns-1; j++)
                {
                    if (PuzzleBoard[i, j+1] == 0)
                        return (i, j);
                }
            }
            return null;
        }

        public (int, int)? GetEmptyTileIndex()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (PuzzleBoard[i, j] == 0) return (i, j);
                }
            }
            // need to find a workaround, too
            return null;
        }

        public bool HasWon()
        {
            if (PuzzleBoard[0, 0] != 1 || PuzzleBoard[Rows - 1, Columns - 1] != 0)
                return false;

            int expectedNumber = 1;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    // if at position 3,3, it skips
                    if (i == Rows - 1 && j == Columns - 1)
                        continue;

                    if (PuzzleBoard[i, j] != expectedNumber)
                        return false;

                    expectedNumber++;
                }
            }
            return true;
        }
    }
}
