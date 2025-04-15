namespace SnakeAndLadderGameLLD.SnakeAndLadderGame{
     public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's play Snake & Ladder");

            //set up board
            int boardSize = 10;
            int numberOfSnakes =10;
            int numberOfLadders =10;
            Board board = new Board(boardSize, numberOfSnakes, numberOfLadders);

            //Set up players
            List<Player>  players = new();
            for(int i=1; i <= 2;i++){
                //create players
                Player player = new Player("Player "+i);
                players.Add(player);
            }   

            //start game;
            Game game  = new Game(board, players, 2);
            Player winner = game.Start();
            Console.WriteLine("Winner is "+ winner.Name);
        }
    }
}
