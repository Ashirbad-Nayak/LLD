
using BlinkItLLD.Payment;
using BlinkItLLD.Domain.Enums;
using BlinkItLLD.Domain;

namespace BlinkItLLD{
    public class PaymentManager{
        private static readonly Object _lock = new Object();
        private static PaymentManager _instance;
        private CartManager _cartManager;
        private IPaymentStrategy _paymentStrategy;
        private PaymentManager()
        {
            // Initialize properties and collections here
            _cartManager = CartManager.Instance;

        }
        public static PaymentManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new PaymentManager();
                    }
                    return _instance;
                }
            }
        }
        public bool MakePayment(int userId, decimal amount, string paymentType)
        {
            // Select the payment strategy based on the payment type
            switch (paymentType)
            {
                case "UPI":
                    _paymentStrategy = new UPIPaymentStrategy();
                    break;
                case "CreditCard":
                    _paymentStrategy = new CreditCardPaymentStrategy();
                    break;
                default:
                    Console.WriteLine("Invalid payment type.");
                    return false;
            }

            // Process the payment
            bool paymentStatus = _paymentStrategy.ProcessPayment(userId, amount);
            UpdateOrder(userId, paymentStatus);
            return paymentStatus;
        }

        
        //clear cart if successful
        //restock store if failure
        private void UpdateOrder(int userId, bool paymentStatus)
        {
                _cartManager.UpdateCartOnPayment(userId, paymentStatus);
                if (paymentStatus)
                {
                    Console.WriteLine("Payment successful. Order placed.");
                }
                else
                {
                    Console.WriteLine("Payment failed. Cart updated.");
                }
        }


    }
}