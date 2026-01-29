using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class Schedule
    {
        private string _scheduleId;    
        private Bus _assignedBus;      
        private Route _assignedRoute;  
        private DateTime _departureTime; 
        private DateTime _arrivalTime;   
        private bool[] _seats = new bool[50]; 
 
        public Schedule(string id, Bus bus, Route route, DateTime deptTime)
        {
            _scheduleId = id;           
            _assignedBus = bus;         
            _assignedRoute = route;     
            _departureTime = deptTime;  
            _arrivalTime = deptTime.AddHours(3); 
           
            for (int i = 0; i < 50; i++)
            {
                _seats[i] = true;  
            }

            _assignedBus.SetAvailability(false);
        }

        public string GetScheduleId() { return _scheduleId; }
        public Bus GetBus() { return _assignedBus; }
        public Route GetRoute() { return _assignedRoute; }
        public DateTime GetDepartureTime() { return _departureTime; }
        public DateTime GetArrivalTime() { return _arrivalTime; }

        public bool HasConflict(Schedule otherSchedule)
        {
            if (this._assignedBus.GetBusId() != otherSchedule._assignedBus.GetBusId())
                return false;

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

        // Get list of booked seats (FOR FILE SAVING)
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