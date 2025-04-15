
namespace SnakeAndLadderGameLLD.SnakeAndLadderGame{
    public class Dice{
        
        public int RollDice(){
            //considering each dice ahs 6 faces
            return new Random().Next(1,6);
        }
    }
}