using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class Route  // Represents a bus route
    {
        // Private fields
        private string _routeId;      // Route ID like "R01"
        private string _destination;  // Final destination
        private List<string> _stops = new List<string>(); // Intermediate stops

        // Constructor
        public Route(string id, string dest)
        {
            _routeId = id;       // Set route ID
            _destination = dest; // Set destination
        }

        // Getter methods
        public string GetRouteId() { return _routeId; }
        public string GetDestination() { return _destination; }
        public List<string> GetStops() { return _stops; }

        // Add a stop to the route
        public void AddStop(string stop)
        {
            _stops.Add(stop);  // Add stop to list
        }

        // Return formatted route information
        public string GetRouteInfo()
        {
            if (_stops.Count == 0)  // If no intermediate stops
                return $"{_routeId} to {_destination} (Direct)";
            else  // If there are stops
                return $"{_routeId} to {_destination} via {string.Join(", ", _stops)}";
        }
    }
}
