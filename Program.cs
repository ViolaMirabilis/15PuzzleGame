namespace _15puzzle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 100);
            Board board = new Board();
            Player player = new Player();

            board.DisplayBoard();
            bool winCheck = board.HasWon();     // false by default
            while (!winCheck)
            {
                var direction = player.GetDirection();
                Console.Clear();

                player.HandleMove(direction, board);
                board.DisplayBoard();
                winCheck = board.HasWon();
            }

            Console.WriteLine("--- You won the 15-Puzzle! ---");
            
        }
    }
}
