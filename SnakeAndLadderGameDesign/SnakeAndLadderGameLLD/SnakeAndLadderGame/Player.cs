namespace SnakeAndLadderGameLLD.SnakeAndLadderGame{
    public class Player{
        public  string Name {get; private set;}
        public int CurrentPosition {get;set;} =0;
        public Player(string name){
            Name =name;
        }
    }
}