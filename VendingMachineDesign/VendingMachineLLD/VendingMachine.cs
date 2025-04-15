using VendingMachineDesign.VendingMachineLLD.States;

namespace VendingMachineDesign.VendingMachineLLD
{
    public class VendingMachine{
        private ItemManager _itemManager;
        private VendingMachineState _state;

        public VendingMachine(){
            _itemManager = ItemManager.Instance;
            _state = new IdleState(this);
        }
        public VendingMachineState GetState(){
            return _state;
        }
        public void SetState(VendingMachineState state){
            _state = state;
        }

        #region Proxy methods to interact with ItemManager
        public void AddItem(string name, double price, int quantity){
            _itemManager.AddItem(name, price, quantity);
        }
        public void RemoveItem(int itemId){
            _itemManager.RemoveItem(itemId);
        }
        public Item GetItem(int itemId){
            return _itemManager.GetItem(itemId);
        }
        public void DisplayItems(){
            foreach (var item in _itemManager.Items.Values.Where(i => i.Quantity > 0)){
                item.DisplayItemInfo();
            }
        }
        public bool IsItemAvailable(int itemId, int quantity){
            return _itemManager.IsItemAvailable(itemId, quantity);
        }

        public void ReduceItemQuantity(int itemId, int quantity){
            if (_itemManager.Items.ContainsKey(itemId)){
                _itemManager.Items[itemId].ReduceQuantity(quantity);
            }
        }
        #endregion

    }
}