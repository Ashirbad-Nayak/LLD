
using VendingMachineDesign.VendingMachineLLD;

namespace VendingMachineDesign.VendingMachineLLD.States
{
    public class VendingMachineState
    {
        protected VendingMachine vendingMachine;
        protected Item item;
        protected int quantity;

        public VendingMachineState(VendingMachine vendingMachine, Item item = null, int quantity = 0)
        {
            this.item = item;
            this.quantity = quantity;
            this.vendingMachine = vendingMachine;
        }



        public virtual void ChooseItem(int itemId, int quantity) { }
        public virtual void AcceptCoin(double amount) { }
        public virtual void DispenseProduct() { }
        public void Exit() { 
            vendingMachine.SetState(new IdleState(vendingMachine));
        }
    }
}