using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class Schedule  // Represents a specific trip
    {
        // Private fields
        private string _scheduleId;    // Schedule ID like "SCH01"
        private Bus _assignedBus;      // Which bus is used
        private Route _assignedRoute;  // Which route
        private DateTime _departureTime; // When it leaves
        private DateTime _arrivalTime;   // When it arrives (3 hours later)
        private bool[] _seats = new bool[50]; // 50 seats, true = available

        // Constructor - creates a new schedule
        public Schedule(string id, Bus bus, Route route, DateTime deptTime)
        {
            _scheduleId = id;           // Set schedule ID
            _assignedBus = bus;         // Assign bus
            _assignedRoute = route;     // Assign route
            _departureTime = deptTime;  // Set departure time
            _arrivalTime = deptTime.AddHours(3); // Arrive 3 hours later

            // Initialize all 50 seats as available (true)
            for (int i = 0; i < 50; i++)
            {
                _seats[i] = true;  // Seat is available
            }

            // Mark bus as in service (not available for other trips)
            _assignedBus.SetAvailability(false);
        }

        // Getter methods
        public string GetScheduleId() { return _scheduleId; }
        public Bus GetBus() { return _assignedBus; }
        public Route GetRoute() { return _assignedRoute; }
        public DateTime GetDepartureTime() { return _departureTime; }
        public DateTime GetArrivalTime() { return _arrivalTime; }

        // **KEY FEATURE: Check if two schedules conflict**
        public bool HasConflict(Schedule otherSchedule)
        {
            // Check if same bus
            if (this._assignedBus.GetBusId() != otherSchedule._assignedBus.GetBusId())
                return false;  // Different buses = no conflict

            // Check if time periods overlap
            // Example: 
            // This schedule: 8:00 AM to 11:00 AM
            // Other schedule: 10:00 AM to 1:00 PM
            // Overlap? Yes (10:00-11:00)
            if (this._departureTime < otherSchedule.GetArrivalTime() &&
                otherSchedule._departureTime < this._arrivalTime)
                return true;  // Time overlap = conflict

            return false;  // No conflict
        }

        // Get list of available seat numbers
        public List<int> GetAvailableSeats()
        {
            List<int> available = new List<int>();  // Create empty list

            // Check each seat (index 0-49)
            for (int i = 0; i < _seats.Length; i++)
            {
                if (_seats[i])  // If seat is available (true)
                {
                    available.Add(i + 1);  // Add seat number (1-50)
                }
            }

            return available;  // Return list of available seats
        }

        // Book a specific seat
        public bool BookSeat(int seatNumber)
        {
            // Check if seat number is valid (1-50)
            if (seatNumber < 1 || seatNumber > 50)
                return false;  // Invalid seat number

            // Check if seat is already booked
            // _seats[0] = seat 1, _seats[1] = seat 2, etc.
            if (!_seats[seatNumber - 1])  // If false (booked)
                return false;  // Seat already taken

            // Book the seat
            _seats[seatNumber - 1] = false;  // Mark as booked
            return true;  // Success
        }

        // Count how many seats are available
        public int GetAvailableSeatCount()
        {
            int count = 0;  // Start count at 0

            // Count true values in seats array
            for (int i = 0; i < _seats.Length; i++)
            {
                if (_seats[i])  // If seat available
                    count++;    // Increase count
            }

            return count;  // Return total available seats
        }

        // Get list of booked seats (optional method)
        public List<int> GetBookedSeats()
        {
            List<int> booked = new List<int>();

            for (int i = 0; i < _seats.Length; i++)
            {
                if (!_seats[i])  // If seat is booked (false)
                {
                    booked.Add(i + 1);  // Add seat number
                }
            }

            return booked;
        }
    }
}