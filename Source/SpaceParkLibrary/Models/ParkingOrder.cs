using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class ParkingOrder
    {
        public int Id { get; set; }
        public Customer CustomerId { get; set; } // Ska vi bara skicka in idnummret som en int istället för objektet
        public Starship StarshipId { get; set; } // Ska vi bara skicka in idnummret som en int istället för objektet
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        public ParkingLot AssignedParkingLot { get; set; } // Ska vi bara skicka in idnummret som en int istället för objektet
        public decimal ParkingFee  { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - KundID: {CustomerId.Id} - SkeppID: {StarshipId.Id} - Ankomst: {ArrivalTime.Hour} - Parkering: {AssignedParkingLot.Id}";
        }
    }
}
