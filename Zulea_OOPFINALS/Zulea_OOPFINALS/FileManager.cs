using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class FileManager
    {
        private string _busesFile = "buses.csv";
        private string _routesFile = "routes.csv";
        private string _schedulesFile = "schedules.csv";
        private string _ticketsFile = "tickets.csv";

        public FileManager()
        {
            EnsureFilesExist();
        }

        private void EnsureFilesExist()
        {
            if (!File.Exists(_busesFile))
            {
                using (StreamWriter sw = File.CreateText(_busesFile)) { }
            }

            if (!File.Exists(_routesFile))
            {
                using (StreamWriter sw = File.CreateText(_routesFile)) { }
            }

            if (!File.Exists(_schedulesFile))
            {
                using (StreamWriter sw = File.CreateText(_schedulesFile)) { }
            }

            if (!File.Exists(_ticketsFile))
            {
                using (StreamWriter sw = File.CreateText(_ticketsFile)) { }
            }
        }

        public List<string> LoadBuses()
        {
            return ReadFile(_busesFile);
        }

        public List<string> LoadRoutes()
        {
            return ReadFile(_routesFile);
        }

        public List<string> LoadSchedules()
        {
            return ReadFile(_schedulesFile);
        }

        public List<string> LoadTickets()
        {
            return ReadFile(_ticketsFile);
        }

        public void SaveBuses(List<string> busData)
        {
            WriteFile(_busesFile, busData);
        }

        public void SaveRoutes(List<string> routeData)
        {
            WriteFile(_routesFile, routeData);
        }

        public void SaveSchedules(List<string> scheduleData)
        {
            WriteFile(_schedulesFile, scheduleData);
        }

        public void SaveTickets(List<string> ticketData)
        {
            WriteFile(_ticketsFile, ticketData);
        }

        private List<string> ReadFile(string filePath)
        {
            List<string> lines = new List<string>();
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            lines.Add(line);
                        }
                    }
                }
                return lines;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading file {filePath}: {e.Message}");
                return lines;
            }
        }

        private void WriteFile(string filePath, List<string> content)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    foreach (string line in content)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error writing to file {filePath}: {e.Message}");
            }
        }
    }
}   