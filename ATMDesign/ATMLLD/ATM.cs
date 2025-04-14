
using ATMLLD.States;

namespace ATMLLD{
    public class ATM{
        private int _2k_Notes;
        private int _500_Notes;
        private int _100_Notes;
        private ATMState state;


        public ATM(int twoKNotes, int fiveHundredNotes, int hundredNotes){
            this._2k_Notes = twoKNotes;
            this._500_Notes = fiveHundredNotes;
            this._100_Notes = hundredNotes;
            this.state = new IdleState(this);
        }
        public void AddNotes(int twoKNotes, int fiveHundredNotes, int hundredNotes){
            this._2k_Notes += twoKNotes;
            this._500_Notes += fiveHundredNotes;
            this._100_Notes += hundredNotes;
        }
        public void Update_2k_Notes(int twoKNotes){
            this._2k_Notes -= twoKNotes;
        }
        public void Update_500_Notes(int fiveHundredNotes){
            this._500_Notes -= fiveHundredNotes;
        }
        public void Update_100_Notes(int hundredNotes){
            this._100_Notes -= hundredNotes;
        }
        public int Get2kNotes(){
            return this._2k_Notes;
        }
        public int Get500Notes(){
            return this._500_Notes;
        }
        public int Get100Notes(){
            return this._100_Notes;
        }

        public void Display(){
            Console.WriteLine("ATM Note Count:");
            Console.WriteLine("2000 Notes: " + this._2k_Notes);
            Console.WriteLine("500 Notes: " + this._500_Notes);
            Console.WriteLine("100 Notes: " + this._100_Notes);
        }

        public void SetState(ATMState state){
            this.state = state;
        }
        public ATMState GetState(){
            return this.state;
        }

        public double GetBalance(){
            return (this._2k_Notes * 2000) + (this._500_Notes * 500) + (this._100_Notes * 100);
        }
    }
}