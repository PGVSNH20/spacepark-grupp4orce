using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class Starship
    {
        // Rymdskepp har en ägare samt den plats den står på, ev. värde för kreditupplysning
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } // Veichle identity number, chassie number
        public string Name { get; set; }

        // Navigation Properties - Här vet vi att ett specifikt skepp kan ha deltagit i flera olika parkeringar
        //public List<ParkingOrder> ParkingOrders { get; set; }


        public Starship()
        {

        }
        public Starship(string reg, string name )
        {
            RegistrationNumber = reg;
            Name = name;
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
