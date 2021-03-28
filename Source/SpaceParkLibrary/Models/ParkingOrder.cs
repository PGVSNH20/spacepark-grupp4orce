﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class ParkingOrder 
    {
        public int Id { get; set; }

        // Navigation properties
        //public int CustomerId { get; set; }
        public Customer Customer { get; set; } 

        //public int StarshipID { get; set; }
        public Starship Starship { get; set; } 

        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        // Navigation properties
        public int AssignedParkingLotId { get; set; }
        //public ParkingLot AssignedParkingLot{ get; set; }

        public decimal ParkingFee  { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - KundID: {Customer.Id} - SkeppID: {Starship.Id} - Ankomst: {ArrivalTime.Hour} - Parkeringsplats {AssignedParkingLotId}";
        }
    }
}
