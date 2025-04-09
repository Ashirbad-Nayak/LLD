using ParkingSystemLLD.Domain;
using ParkingSystemLLD.Domain.Enums;
using ParkingSystemLLD.ParkingSpotManagerWithFactory;
using ParkingSystemLLD.ParkingSpotManagerWithFactoryWithFactory;
using ParkingSystemLLD.PaymentStrategy;
using ParkingSystemLLD.PriceCalculatorStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD
{
    public class ExitGate
    {
        private static ExitGate _exitGateInstance;
        private static readonly object _lock = new object();

        private ParkingSpotManager parkingSpotManager;
        private ReservationManager reservationManager;
        private IPriceCalculatorStrategy priceCalculatorStrategy;
        private IPaymentStrategy paymentStrategy;

        public ExitGate(ParkingSpotTypeEnum parkingSpotType)
        {
            InitializeParkingSpotManager(parkingSpotType);
            InitializeReservationManager();
            InitializePriceCalculatorStrategy();
            InitializePaymentStrategy();
        }

        
        private void InitializeParkingSpotManager(ParkingSpotTypeEnum parkingSpotType)
        {
            // Initialize the ParkingSpotManager here
            parkingSpotManager = ParkingSpotManagerFactory.GetParkingSpotManager(parkingSpotType);
        }

        private void InitializeReservationManager()
        {
            // Initialize the ReservationManager here
            reservationManager = ReservationManager.GetReservationManagerInstane();
        }

        private void InitializePriceCalculatorStrategy()
        {
            // Initialize the PriceCalculatorStrategy here
            priceCalculatorStrategy = new DefaultPriceCalculatorStrategy();
        }

        private void InitializePaymentStrategy()
        {
            // Initialize the PaymentStrategy here
            paymentStrategy = new UPIPaymentStrategy();
        }

        public Reservation GetReservation(Vehicle vehicleInfo)
        {
            return reservationManager.GetReservation(vehicleInfo.VehicleNumber);
        }

        public double CalculatePrice(Reservation reservation)
        {
            return priceCalculatorStrategy.CalculatePrice(reservation);
        }

        public void MakePayment(Reservation reservation, double amount)
        {
            // Implement payment logic here
            paymentStrategy.ProcessPayment(amount);

        }

        public void ReleaseParkingSpot(int parkingSpotId)
        {
            parkingSpotManager.ReleaseParkingSpot(parkingSpotId);
        }
        public void UpdateReservation(string vehicleNumber)
        {
            reservationManager.UpdateReservation(vehicleNumber);
        }
    }
}
