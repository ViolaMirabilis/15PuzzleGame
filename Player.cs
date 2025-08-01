using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static _15puzzle.Player;

namespace _15puzzle
{
    public class Player
    {

        public int Row { get; set; }
        public int Col { get; set; }
        public Direction GetDirection()
        {
            while(true)
            {
                Console.WriteLine("Where to move? [up, down, left, right]");
                var input = Console.ReadKey(false).Key;

                return input switch
                {
                    ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.DownArrow => Direction.Down,
                    ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.RightArrow => Direction.Right
                };
            }
        }

        public void HandleMove(Direction direction, Board board)
        {
            switch (direction)
            {
                case Direction.Up:
                    MoveUp(board);
                    break;
                case Direction.Down:
                    MoveDown(board);
                    break;
                case Direction.Left:
                    MoveLeft(board);
                    break;
                case Direction.Right:
                    MoveRight(board);
                    break; 
            }
                
        }

        // VALUE is needed because the value might be null here
        public void MoveUp(Board board)
        {
            var movableTileIndex = board.MoveUpCoordinates();           // gets coordinates of a tile that can be moved up (int row, int column)
            // null check
            if (movableTileIndex != null)
            {
                ReplaceNumbers(board, movableTileIndex);
            }
        }

        public void MoveDown(Board board)
        {
            var movableTileIndex = board.MoveDownCoordinates();           // gets coordinates of a tile that can be moved up (int row, int column)
            
            if (movableTileIndex != null)
            {
                ReplaceNumbers(board, movableTileIndex);
            }
        }

        public void MoveLeft(Board board)
        {
            var movableTileIndex = board.MoveLeftCoordinates();           // gets coordinates of a tile that can be moved up (int row, int column)

            if (movableTileIndex != null)
            {
                ReplaceNumbers(board, movableTileIndex);
            }
        }

        public void MoveRight(Board board)
        {
            var movableTileIndex = board.MoveRightCoordinates();           // gets coordinates of a tile that can be moved up (int row, int column)

            if (movableTileIndex != null)
            {
                ReplaceNumbers(board, movableTileIndex);
            }
        }


        public void ReplaceNumbers(Board board, (int, int)? movableTileIndex)
        {
            var emptyTileCoordinates = board.GetEmptyTileIndex();       // gets empty tile coordinates (int row, int column)
            var movableTileValue = board.PuzzleBoard[movableTileIndex.Value.Item1, movableTileIndex.Value.Item2];   // value of the number that is going to be moved up

            board.PuzzleBoard[emptyTileCoordinates.Value.Item1, emptyTileCoordinates.Value.Item2] = movableTileValue;   // empty tile becomes a full tile
            board.PuzzleBoard[movableTileIndex.Value.Item1, movableTileIndex.Value.Item2] = 0;
        }



        // accepts a tuple
        public bool MoveInDirection(Board board, (int, int) number)      // int row, int column
        {
            // i = rows; j = columns
            int r = number.Item1;
            int c = number.Item2;
            var direction = GetDirection();


            // if user types UP and the checks go through, they can move there
            if (direction == Direction.Up && c > 0 && board.PuzzleBoard[r, c - 1] == 0) return true;       // neighbour on top
            if (direction == Direction.Down && c < 3 && board.PuzzleBoard[r, c + 1] == 0) return true;       // neighbour on the bottom
            if (direction == Direction.Left && r > 0 && board.PuzzleBoard[r - 1, c] == 0) return true;       // neighbour on the left
            if (direction == Direction.Right && r < 0 && board.PuzzleBoard[r + 1, c] == 0) return true;       // neighbour on the right

            return false;
        }

        public enum Direction { Up, Down, Left, Right};
    }
}
