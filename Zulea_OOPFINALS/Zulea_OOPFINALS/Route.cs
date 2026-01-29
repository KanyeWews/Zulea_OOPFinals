using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class Route  
    {
        private string _routeId;      
        private string _destination;  
        private List<string> _stops = new List<string>(); 

        public Route(string id, string dest)
        {
            _routeId = id;       
            _destination = dest; 
        }

        public string GetRouteId() { return _routeId; }
        public string GetDestination() { return _destination; }
        public List<string> GetStops() { return _stops; }

        public void AddStop(string stop)
        {
            _stops.Add(stop);  
        }

        public string GetRouteInfo()
        {
            if (_stops.Count == 0)  
                return $"{_routeId} to {_destination} (Direct)";
            else  
                return $"{_routeId} to {_destination} via {string.Join(", ", _stops)}";
        }
    }
}
