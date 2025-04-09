using ParkingSystemLLD.Domain;
using ParkingSystemLLD.Domain.Enums;
using ParkingSystemLLD.ParkingSpotManagerWithFactory;
using ParkingSystemLLD.ParkingSpotManagerWithFactoryWithFactory;

namespace ParkingSystemLLD
{
   public class EntryGate
    {
        private static readonly Object _lock = new Object();
        private ParkingSpotManager parkingSpotManager;
        private ReservationManager reservationManager;
        private static EntryGate _entranceGateInstance;

        public   EntryGate(ParkingSpotTypeEnum parkingSpotType)
        {
            //private constructor
            InitializeParkingSpotManager(parkingSpotType);
            InitializeReservationManager();
        }

        public void InitializeParkingSpotManager(ParkingSpotTypeEnum parkingSpotType)
        {
            parkingSpotManager = ParkingSpotManagerFactory.GetParkingSpotManager(parkingSpotType);
        }

        public void InitializeReservationManager()
        {
            reservationManager = ReservationManager.GetReservationManagerInstane();
        }

        public List<ParkingSpot> GetAvailableParkingSpots()
        {
           return  parkingSpotManager.GetParkingSpots();
        }

        public void BookParkingSpot(int Id)
        {
            parkingSpotManager.BookParkingSpot(Id);

        }

        public Reservation CreateReservation(Vehicle vehicle, ParkingSpot parkingSpot)
        {
           return  reservationManager.CreateReservation(vehicle, parkingSpot);

        }
    }
}
