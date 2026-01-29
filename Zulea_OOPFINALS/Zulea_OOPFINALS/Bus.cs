using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class Bus  
    {
        private string _busId;        
        private string _licensePlate; 
        private int _totalSeats;      
        private bool _isAvailable;    
        private string _driverName;   
        
        public Bus(string id, string plate, int seats)
        {
            _busId = id;           
            _licensePlate = plate; 
            _totalSeats = seats;   
            _isAvailable = true;   
            _driverName = "No Driver"; 
        }

        public string GetBusId() { return _busId; }
        public string GetLicensePlate() { return _licensePlate; }
        public int GetTotalSeats() { return _totalSeats; }
        public bool IsAvailable() { return _isAvailable; }
        public string GetDriverName() { return _driverName; }
        public void SetAvailability(bool status) { _isAvailable = status; }
        public void AssignDriver(string driver) { _driverName = driver; }
    }
}