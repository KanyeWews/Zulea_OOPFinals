using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class Bus  // Represents a physical bus
    {
        // Private fields - same naming convention as Blackjack (_camelCase)
        private string _busId;        // Unique ID like "BUS01"
        private string _licensePlate; // License plate number
        private int _totalSeats;      // Total seats on bus
        private bool _isAvailable;    // True if bus is free
        private string _driverName;   // Driver's name
        
        // Constructor - called when creating new Bus object
        public Bus(string id, string plate, int seats)
        {
            _busId = id;           // Set bus ID
            _licensePlate = plate; // Set license plate
            _totalSeats = seats;   // Set seat count
            _isAvailable = true;   // New bus starts as available
            _driverName = "No Driver"; // Default driver
        }
        
        // Getter methods - public way to access private fields
        public string GetBusId() { return _busId; }
        public string GetLicensePlate() { return _licensePlate; }
        public int GetTotalSeats() { return _totalSeats; }
        public bool IsAvailable() { return _isAvailable; }
        public string GetDriverName() { return _driverName; }
        
        // Setter methods - change field values
        public void SetAvailability(bool status) { _isAvailable = status; }
        public void AssignDriver(string driver) { _driverName = driver; }
    }
}