using SpaceParkLibrary.Models;
using SpaceParkLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Interfaces
{
    public interface IFluentCustomer
    {
        int Id { get; set; }
        string Name { get; set; }
        public string Email { get; set; }
        public ParkingOrder ParkingRegistration { get; set; }

        Task<IFluentCustomer> SelectStarship();
        IFluentCustomer VisitParkingHouse(ParkingHouse parkingHouse);
        Task<IFluentCustomer> SelfRegistration();
        IFluentCustomer ParkShip(DateTime arrivalTime);
        IFluentCustomer DoingStuffOutsideParkingHousePerMinute(int minutes);
        IFluentCustomer LeavePark(DateTime departureTime);
        IFluentCustomer DisplayCreditWorthiness(); // Tar nog bort den här så får LeaveParksköta det ist
        IFluentCustomer ReceiveInvoice();


    }
}
