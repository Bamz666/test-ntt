using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    public class Vehicle
    {
        public string RegistrationNumber { get; }
        public string Color { get; }
        public string Type { get; }

        public Vehicle(string registrationNumber, string color, string type)
        {
            RegistrationNumber = registrationNumber;
            Color = color;
            Type = type;
        }
    }

    public class ParkingLot
    {
        private readonly int _totalSlots;
        private readonly List<Vehicle> _vehicles;

        public ParkingLot(int totalSlots)
        {
            _totalSlots = totalSlots;
            _vehicles = new List<Vehicle>();
            Console.WriteLine($"Created a parking lot with {_totalSlots} slots");
        }

        public void Park(Vehicle vehicle)
        {
            if (_vehicles.Count >= _totalSlots)
            {
                Console.WriteLine("Sorry, parking lot is full");
                return;
            }

            _vehicles.Add(vehicle);
            Console.WriteLine($"Allocated slot number: {_vehicles.Count}");
        }

        public void Leave(int slotNumber)
        {
            if (slotNumber <= 0 || slotNumber > _vehicles.Count)
            {
                Console.WriteLine("Invalid slot number");
                return;
            }

            _vehicles.RemoveAt(slotNumber - 1);
            Console.WriteLine($"Slot number {slotNumber} is free");
        }

        public void Status()
        {
            Console.WriteLine("Slot\tNo.\tType\tRegistration No\tColor");
            for (int i = 0; i < _vehicles.Count; i++)
            {
                var vehicle = _vehicles[i];
                Console.WriteLine($"{i + 1}\t{vehicle.RegistrationNumber}\t{vehicle.Type}\t{vehicle.Color}");
            }
        }

        public void TypeOfVehicles(string type)
        {
            var count = _vehicles.Count(v => v.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(count);
        }

        public void RegistrationNumbersForVehiclesWithPlate(string plate)
        {
            var vehicles = _vehicles.Where(v => v.RegistrationNumber.StartsWith(plate, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(string.Join(", ", vehicles.Select(v => v.RegistrationNumber)));
        }

        public void RegistrationNumbersForVehiclesWithColor(string color)
        {
            var vehicles = _vehicles.Where(v => v.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(string.Join(", ", vehicles.Select(v => v.RegistrationNumber)));
        }

        public void SlotNumbersForVehiclesWithColor(string color)
        {
            var slots = _vehicles.Where(v => v.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                                 .Select((v, index) => index + 1);
            Console.WriteLine(string.Join(", ", slots));
        }

        public void SlotNumberForRegistrationNumber(string registrationNumber)
        {
            var vehicle = _vehicles.FirstOrDefault(v => v.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
            if (vehicle != null)
                Console.WriteLine(_vehicles.IndexOf(vehicle) + 1);
            else
                Console.WriteLine("Not found");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ParkingLot parkingLot = null;

            while (true)
            {
                Console.Write("$ ");
                var input = Console.ReadLine()?.Split(' ');

                if (input == null || input.Length == 0)
                    continue;

                var command = input[0].ToLower();

                switch (command)
                {
                    case "create_parking_lot":
                        if (input.Length != 2 || !int.TryParse(input[1], out int totalSlots))
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        parkingLot = new ParkingLot(totalSlots);
                        break;
                    case "park":
                        if (input.Length != 4 || parkingLot == null)
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        var vehicle = new Vehicle(input[1], input[3], input[2]);
                        parkingLot.Park(vehicle);
                        break;
                    case "leave":
                        if (input.Length != 2 || parkingLot == null || !int.TryParse(input[1], out int slotNumberLeave))
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        parkingLot.Leave(slotNumberLeave);
                        break;
                    case "status":
                        if (parkingLot == null)
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        parkingLot.Status();
                        break;
                    case "type_of_vehicles":
                        if (input.Length != 2 || parkingLot == null)
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        parkingLot.TypeOfVehicles(input[1]);
                        break;
                    case "registration_numbers_for_vehicles_with_plate":
                        if (input.Length != 2 || parkingLot == null)
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        parkingLot.RegistrationNumbersForVehiclesWithPlate(input[1]);
                        break;
                    case "registration_numbers_for_vehicles_with_color":
                        if (input.Length != 2 || parkingLot == null)
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        parkingLot.RegistrationNumbersForVehiclesWithColor(input[1]);
                        break;
                    case "slot_numbers_for_vehicles_with_color":
                        if (input.Length != 2 || parkingLot == null)
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        parkingLot.SlotNumbersForVehiclesWithColor(input[1]);
                        break;
                    case "slot_number_for_registration_number":
                        if (input.Length != 2 || parkingLot == null)
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        parkingLot.SlotNumberForRegistrationNumber(input[1]);
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }
    }
}
