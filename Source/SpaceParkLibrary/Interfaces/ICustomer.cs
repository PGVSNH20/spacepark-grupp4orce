﻿using SpaceParkLibrary.Models;
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
     
        Task<IFluentCustomer> SelectStarship(Starship starship);
        IFluentCustomer VisitParkingHouse(ParkingHouse parkingHouse);
        Task<IFluentCustomer> SelfRegistration(Starship starship, ParkingOrder parkingOrder);
        IFluentCustomer ParkShip(Starship vechicle, DateTime arrivalTime, ParkingOrder parkingOrder);
        IFluentCustomer LeavePark(DateTime departureTime, ParkingOrder parkingOrder);
        IFluentCustomer DisplayCreditWorthiness(); // Tar nog bort den här så får LeaveParksköta det ist
        IFluentCustomer ReceiveInvoice();


    }
}
