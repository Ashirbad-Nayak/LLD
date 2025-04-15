

namespace SnakeAndLadderGameLLD.SnakeAndLadderGame{
    public class Board{
        private Cell[][] board;
        private int boardSize;
        private int numberOfSnakes;
        private int numberOfLadders;

        public Board(int boardSize, int numberOfSnakes, int numberOfLadders){

            this.boardSize = boardSize;
            this.numberOfSnakes = numberOfSnakes;
            this.numberOfLadders = numberOfLadders;
            IntializeBoard();
        }

        private void IntializeBoard(){
            //create board

            board = new Cell[boardSize][];
            for(int i=0; i<boardSize; i++){
                board[i] = new Cell[boardSize];
                Array.Fill(board[i], new Cell());
            }
            // for(int i=0; i< boardSize; i++){
            //     for(int j = 0; j < boardSize; j++){
            //         board[i][j] = new Cell();
            //     }
            // }
            InitializeSnakesAndLadders();
        }

        private void InitializeSnakesAndLadders(){
            Console.WriteLine(new string('=',50));

            Random random = new Random();
            //create snakes 
            while(numberOfSnakes > 0 || numberOfLadders > 0){
                //find 2 randome numbers
                int start = random.Next(0,boardSize * boardSize-1);
                int end  = random.Next(0,boardSize * boardSize-1);

                if(start>end && numberOfSnakes >0){
                    numberOfSnakes--;
                    (int row , int col) = FindRowAndColumn(start);
                    Link link = new Link(end);
                    board[row][col].link = link;

                    Console.WriteLine($"Snake: from {start} to {end}");
                }
                else if(start < end && numberOfLadders >0){
                    numberOfLadders--;
                    (int row , int col) = FindRowAndColumn(start);
                    Link link = new Link(end);
                    board[row][col].link = link;
                    Console.WriteLine($"Ladder: from {start} to {end}");

                }

            }
            Console.WriteLine(new string('=',50));
            Console.ReadLine();

        }

        public (int,int) FindRowAndColumn(int number){
            int row = number/boardSize;
            int col =-1;
            //if even row
            if(row % 2 == 0)
                col = number%boardSize;
            else
                col = boardSize - (number % boardSize)-1;

            return (row,col);

        }

        public Cell[][] GetBoard(){
            return board;
        }
        
    }
}