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

        Task<Starship> RegistrateStarship();
        Task<IFluentCustomer> SelfRegistration();
        IFluentCustomer ParkShip(DateTime arrivalTime);
        Task<Double> ParkingTimeInMinutesSimulator(int minutes);
        Task<IFluentCustomer> LeavePark(DateTime departureTime);

    }
}
