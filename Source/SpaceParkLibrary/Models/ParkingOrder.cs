using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class ParkingOrder
    {
        public Starship Starship { get; set; } // Vara eller icke vara?

        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        public ParkingLot AssignedParkingLot { get; set; }
        public decimal ParkingFee  { get; set; }
        public int Id { get; set; }


    }
}
