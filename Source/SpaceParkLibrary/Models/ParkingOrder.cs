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
        public Customer Customer { get; set; } // Ska vi bara skicka in idnummret som en int istället för objektet
        public Starship Starship { get; set; } // Ska vi bara skicka in idnummret som en int istället för objektet
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        
        public int AssignedParkingLotId { get; set; } // Ska vi bara skicka in idnummret som en int istället för objektet
        public decimal ParkingFee  { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - KundID: {Customer.Id} - SkeppID: {Starship.Id} - Ankomst: {ArrivalTime.Hour} - Parkeringsplats {AssignedParkingLotId}";
        }
    }
}
