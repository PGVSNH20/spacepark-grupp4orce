using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class ParkingHouse
    {
        // Parkeringshus, totala platser, lista med platser
        // Samt räknare för lediga platser som skickas till dörrvakten

        private static byte _vacantParkingLotsCounter;

        private static readonly byte maximumParkingLots = 10;

        public ParkingHouse(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public byte TotalParkingsLots { get; set; } = (byte)parkingsLots.Count;

        public byte VacantParkingLots { get; set; } = (byte)parkingsLots.Where(p => p.Occupied == false).Count();

        public static int CustomerCounter { get; set; }

        public bool Vacancy { get; set; } = true; // Måste beräknas först, men sätter som true just nu. 

        List<ParkingLot> ParkingLots { get; set; } = parkingsLots;

        private static List<ParkingLot> parkingsLots = new List<ParkingLot>
        {
            new ParkingLot(1, true),
            new ParkingLot(2, false),
            new ParkingLot(3, true),
            new ParkingLot(4, true),
            new ParkingLot(5, false),
            new ParkingLot(6, false),
            new ParkingLot(7, true),
            new ParkingLot(8, false),
            new ParkingLot(10, true),
            new ParkingLot(8, true)

        };

        public ParkingLot GetEmptyParkingLot()
        {
            //ParkingLot assignedEmptyLot;
            if (Vacancy)
            {

                int indexOfEmptyLot = parkingsLots.IndexOf(parkingsLots.Where(p => p.Occupied == false).First());

                parkingsLots[indexOfEmptyLot].Occupied = true;


                return parkingsLots[indexOfEmptyLot];

            }
            else return null;


        }

        //List<Char> Sections { get; set; } = new List<char>() { 'A', 'B', 'C', 'D' };

    }
}
