using ATMLLD;
namespace ATMLLD.States{
    public  class ATMState {

        protected ATM atm;
        protected Card card;
        public ATMState(ATM atm, Card card){
            this.atm = atm;
            this.card = card;
        }

    #region basics of inheritance

    // The reason we marked the class as a normal class and its methods virtual  is  because
    // we want to allow the derived classes to override specific methods(not forced to override all) and provide their own implementations.

    // Also ,If the class were abstract, it would force all derived classes to implement all methods, which may not be necessary for every derived class.

    // Remember that
    // if we have base class and  a derived class with their own methods
    // we create derived class object, we can assign it to Baseclass obj.
    // The base class obj can only access/call those methods which are declared in the base class and thsose methods which are overridden in the derived class.    
    // not the Methods specific to derived class

    // So, just deriving and assigning the derived class object to base class object cannot be used to access methods 
    // specific to the derived class. 
    //Thats why in interface , we are able to assign the derived class object to Interface object  in High level module 
    // and still can access the methods of the derived class.
    #endregion


        // //Idle
        public virtual void InsertCard(Card card){
            //dummy implementation
        }

        // //HasCard
        public virtual void EnterPin(string pin){
            //dummy implementation
        }

        // //SelectOperation
        public  virtual void HandleInput(string operation){
            //dummy implementation
        }

        // //Withdraw
        public  virtual void Withdraw(string amount){

        }

        //CheckBalance
        public  virtual void CheckBalance(){
            //dummy implementation
        }

        // //Exit
        public void Exit()
        {
            Console.WriteLine("Exiting ATM. Thank you for using our service.");
            Console.WriteLine("Please take your card.");
            // Reset the ATM state to idle
            atm.SetState(new IdleState(atm));
        }


    }
}