namespace SnakeAndLadderGameLLD.SnakeAndLadderGame{

    public class Game{
        private Board board;
        private List<Player> players = new List<Player>();
        private Player winner;
        private int numberOfDice ;
        
        public Game(Board board, List<Player> players, int numberOfDice){
            this.board =board;
            this.players.AddRange(players);
            this.numberOfDice = numberOfDice;
        }

        public Player Start(){
                int i = 0;
                int dices =numberOfDice;
                Dice dice =new Dice();
                Cell[][] currentBoard = board.GetBoard();

                while(winner is null){
                    DisplayPlayerPositions();
                    Player player = players[i];
                    Console.WriteLine("Player Turn: "+ player.Name);
                    //roll dice  x times: x= numberOfDice
                    //or roll x dice
                    int diceOutput =0;
                    while(dices > 0){
                        dices--;
                        diceOutput+= dice.RollDice();
                    }
                    Console.WriteLine($"{player.Name} : Dice Output : {diceOutput}");
                    dices = numberOfDice;
                    
                    player.CurrentPosition  += diceOutput;
                    
                    GoToNewPosition(player, currentBoard);//check if snake or ladder is present

                    
                    if(player.CurrentPosition >= currentBoard.Length * currentBoard.Length)
                        {
                            winner = player;
                            return winner;
                        }
                        i = FindPlayerTurn(i);
                }
                return null;

        }

        private int FindPlayerTurn(int i){
                if (i < players.Count -1 ) return ++i;
                return 0;
        }

        private void GoToNewPosition(Player player, Cell[][] currentBoard){
                    //check if snake or ladder is present
                    if(player.CurrentPosition < currentBoard.Length * currentBoard.Length ){
                       (int row, int col) = board.FindRowAndColumn(player.CurrentPosition);
                       if(currentBoard[row][col].link is not null &&  currentBoard[row][col].link.end != -1){
                        
                            //snake present
                            if(currentBoard[row][col].link.end < player.CurrentPosition)
                            {
                                Console.WriteLine($"Snake Present. Moving  {player.Name} from {player.CurrentPosition} to {currentBoard[row][col].link.end}");
                                
                            }else{
                                //ladder present
                                Console.WriteLine($"Ladder Present. Moving  {player.Name} from {player.CurrentPosition} to {currentBoard[row][col].link.end}");
                                
                            }
                            player.CurrentPosition = currentBoard[row][col].link.end;
                            }
                       }
          
        }
        public void DisplayPlayerPositions(){
            foreach(Player player in players){
                Console.WriteLine($"{player.Name} is at {player.CurrentPosition}");
            }
        }



    }
}