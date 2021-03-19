using SpaceParkLibrary.Models;
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
        Starship Starship { get; set; }
        DateTime ArrivalTime { get; set; }
        DateTime DepartureTime { get; set; }
        bool CreditWorthiness { get; set; } // Kanske null direkt?
        bool SelfRegistrated { get; set; }


        IFluentCustomer SelfRegistration();
        IFluentCustomer ParkShip(Starship vechicle, DateTime arrivalTime);
        IFluentCustomer LeavePark(DateTime departureTime);
        IFluentCustomer DisplayCreditWorthiness(); // Tar nog bort den här så får LeaveParksköta det ist
        IFluentCustomer ReciveInvoice();


    }
}
