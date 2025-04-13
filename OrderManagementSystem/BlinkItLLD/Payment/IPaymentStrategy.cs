

namespace BlinkItLLD.Payment
{
    public interface IPaymentStrategy
    {
        bool ProcessPayment(int cartId, decimal amount);
    }
}
