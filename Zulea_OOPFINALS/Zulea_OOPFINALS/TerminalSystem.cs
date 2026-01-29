using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class TerminalSystem
    {
        private List<Bus> _buses = new List<Bus>();
        private List<Route> _routes = new List<Route>();
        private List<Schedule> _schedules = new List<Schedule>();
        private List<Ticket> _tickets = new List<Ticket>();
        private FileManager _dataManager;   

        public TerminalSystem()
        {
            Console.WriteLine("=== BUS TERMINAL SYSTEM ===\n");
            _dataManager = new FileManager();
            LoadSystemData();

            if (_buses.Count == 0)
            {
                Console.WriteLine("No data found. Creating sample data...\n");
                CreateSampleData();
                SaveSystemData(); 
                Console.ReadKey();
            }
            RunMainMenu();
        }

        private void CreateSampleData()
        {
            Console.WriteLine("Creating comprehensive sample data...\n");

            // ========== CREATE MORE BUSES ==========
            Bus bus1 = new Bus("BUS01", "ABC-123", 50);
            Bus bus2 = new Bus("BUS02", "DEF-456", 45);
            Bus bus3 = new Bus("BUS03", "GHI-789", 55);
            Bus bus4 = new Bus("BUS04", "JKL-012", 40);
            Bus bus5 = new Bus("BUS05", "MNO-345", 50); // This bus will be AVAILABLE

            bus1.AssignDriver("John Smith");
            bus2.AssignDriver("Maria Garcia");
            bus3.AssignDriver("Robert Johnson");
            bus4.AssignDriver("Lisa Wong");
            bus5.AssignDriver("David Chen"); // Available driver

            _buses.Add(bus1);
            _buses.Add(bus2);
            _buses.Add(bus3);
            _buses.Add(bus4);
            _buses.Add(bus5);

            // ========== CREATE MORE ROUTES ==========
            Route route1 = new Route("R01", "EDSA Carousel");
            route1.AddStop("Caloocan");
            route1.AddStop("Makati");
            route1.AddStop("Mandaluyong");
            route1.AddStop("Paranaque");
            route1.AddStop("Pasay");

            Route route2 = new Route("R02", "San Francisco");
            route2.AddStop("Sacramento");
            route2.AddStop("San Jose");

            Route route3 = new Route("R03", "QC Loop");
            route3.AddStop("Cubao");
            route3.AddStop("Katipunan");
            route3.AddStop("Fairview");
            route3.AddStop("Novaliches");

            Route route4 = new Route("R04", "Manila Bay");
            route4.AddStop("Luneta");
            route4.AddStop("Intramuros");
            route4.AddStop("MOA");
            route4.AddStop("Baclaran");

            Route route5 = new Route("R05", "Airport Express");
            route5.AddStop("NAIA Terminal 1");
            route5.AddStop("NAIA Terminal 2");
            route5.AddStop("NAIA Terminal 3");
            route5.AddStop("NAIA Terminal 4");

            _routes.Add(route1);
            _routes.Add(route2);
            _routes.Add(route3);
            _routes.Add(route4);
            _routes.Add(route5);

            // ========== CREATE SCHEDULES WITH FIXED DATES ==========
            // Use a fixed date (e.g., December 20, 2024)
            DateTime fixedDate = new DateTime(2024, 12, 20); // Any date you want

            // === MORNING SCHEDULES (6 AM - 11 AM) ===
            // Assign buses 1-4 only, keep bus 5 AVAILABLE
            Schedule schedule1 = new Schedule("SCH01", bus1, route1, fixedDate.AddHours(6));   // 6:00 AM
            Schedule schedule2 = new Schedule("SCH02", bus2, route2, fixedDate.AddHours(7).AddMinutes(30)); // 7:30 AM
            Schedule schedule3 = new Schedule("SCH03", bus3, route3, fixedDate.AddHours(8).AddMinutes(15)); // 8:15 AM
            Schedule schedule4 = new Schedule("SCH04", bus4, route4, fixedDate.AddHours(9));   // 9:00 AM
            Schedule schedule5 = new Schedule("SCH05", bus1, route5, fixedDate.AddHours(10).AddMinutes(45)); // 10:45 AM (reuse bus1)

            // === AFTERNOON SCHEDULES (12 PM - 4 PM) ===
            // Reuse buses 1-4, keep bus 5 AVAILABLE
            Schedule schedule6 = new Schedule("SCH06", bus2, route2, fixedDate.AddHours(12));  // 12:00 PM
            Schedule schedule7 = new Schedule("SCH07", bus3, route3, fixedDate.AddHours(13).AddMinutes(30)); // 1:30 PM
            Schedule schedule8 = new Schedule("SCH08", bus4, route4, fixedDate.AddHours(14).AddMinutes(15)); // 2:15 PM
            Schedule schedule9 = new Schedule("SCH09", bus1, route5, fixedDate.AddHours(15).AddMinutes(45)); // 3:45 PM

            // === EVENING SCHEDULES (5 PM - 8 PM) ===
            // Still not using bus 5 - KEEP IT AVAILABLE
            Schedule schedule10 = new Schedule("SCH10", bus2, route1, fixedDate.AddHours(17)); // 5:00 PM
            Schedule schedule11 = new Schedule("SCH11", bus3, route3, fixedDate.AddHours(18).AddMinutes(30)); // 6:30 PM
            Schedule schedule12 = new Schedule("SCH12", bus4, route4, fixedDate.AddHours(19).AddMinutes(15)); // 7:15 PM

            // === NIGHT SCHEDULES (9 PM - 11 PM) ===
            // Use bus 3 and 4 only, KEEP BUS 5 AVAILABLE
            Schedule schedule13 = new Schedule("SCH13", bus3, route5, fixedDate.AddHours(21)); // 9:00 PM
            Schedule schedule14 = new Schedule("SCH14", bus4, route2, fixedDate.AddHours(22).AddMinutes(30)); // 10:30 PM

            // ========== ADD ALL SCHEDULES ==========
            _schedules.Add(schedule1);
            _schedules.Add(schedule2);
            _schedules.Add(schedule3);
            _schedules.Add(schedule4);
            _schedules.Add(schedule5);
            _schedules.Add(schedule6);
            _schedules.Add(schedule7);
            _schedules.Add(schedule8);
            _schedules.Add(schedule9);
            _schedules.Add(schedule10);
            _schedules.Add(schedule11);
            _schedules.Add(schedule12);
            _schedules.Add(schedule13);
            _schedules.Add(schedule14);

            // ========== BOOK SOME SEATS ==========
            // Morning buses - more crowded
            schedule1.BookSeat(5); schedule1.BookSeat(12); schedule1.BookSeat(23);
            schedule2.BookSeat(8); schedule2.BookSeat(15); schedule2.BookSeat(30);
            schedule3.BookSeat(10); schedule3.BookSeat(20); schedule3.BookSeat(35);

            // Afternoon buses
            schedule6.BookSeat(7); schedule6.BookSeat(14);
            schedule7.BookSeat(12); schedule7.BookSeat(25);

            // Evening buses - rush hour
            schedule10.BookSeat(15); schedule10.BookSeat(30); schedule10.BookSeat(45);
            schedule11.BookSeat(20); schedule11.BookSeat(40);

            // Night buses - less crowded
            schedule13.BookSeat(5); schedule13.BookSeat(10);
            schedule14.BookSeat(8);

            Console.WriteLine("✓ Created 5 buses");
            Console.WriteLine("  • BUS01-BUS04: Assigned to schedules (In Service)");
            Console.WriteLine("  • BUS05: AVAILABLE for new schedules ✓");
            Console.WriteLine("✓ Created 5 routes");
            Console.WriteLine("✓ Created 14 schedules (6 AM - 10:30 PM)");
            Console.WriteLine("✓ Booked seats realistically");
            Console.WriteLine($"✓ All schedules are for: {fixedDate:MMMM dd, yyyy}");
            Console.WriteLine("\nIMPORTANT: BUS05 is AVAILABLE for testing 'Add Schedule' feature!");
            Console.WriteLine("✓ Sample data created successfully!");
        }

        private void LoadSystemData()
        {
            Console.WriteLine("Loading system data...\n");

            // Load BusData from CSV
            List<string> busData = _dataManager.LoadBuses();
            foreach (string line in busData)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 5)
                    {
                        Bus bus = new Bus(parts[0], parts[1], int.Parse(parts[2]));
                        bus.SetAvailability(bool.Parse(parts[3]));
                        bus.AssignDriver(parts[4]);
                        _buses.Add(bus);
                    }
                }
            }

            // Load routes from CSV
            List<string> routeData = _dataManager.LoadRoutes();
            foreach (string line in routeData)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 2)
                    {
                        Route route = new Route(parts[0], parts[1]);
                        for (int i = 2; i < parts.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(parts[i]))
                                route.AddStop(parts[i]);
                        }
                        _routes.Add(route);
                    }
                }
            }

            // Load schedules from CSV
            List<string> scheduleData = _dataManager.LoadSchedules();
            foreach (string line in scheduleData)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 5)
                    {
                        string scheduleId = parts[0];
                        string busId = parts[1];
                        string routeId = parts[2];
                        DateTime departureTime = DateTime.Parse(parts[3]);
                        string bookedSeats = parts[4];

                        // Find bus and route
                        Bus bus = _buses.Find(b => b.GetBusId() == busId);
                        Route route = _routes.Find(r => r.GetRouteId() == routeId);

                        if (bus != null && route != null)
                        {
                            Schedule schedule = new Schedule(scheduleId, bus, route, departureTime);

                            // Mark booked seats
                            if (!string.IsNullOrWhiteSpace(bookedSeats))
                            {
                                string[] seats = bookedSeats.Split('|');
                                foreach (string seat in seats)
                                {
                                    if (int.TryParse(seat, out int seatNum))
                                        schedule.BookSeat(seatNum);
                                }
                            }

                            _schedules.Add(schedule);
                        }
                    }
                }
            }

            // Load tickets from CSV
            List<string> ticketData = _dataManager.LoadTickets();
            foreach (string line in ticketData)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 5)
                    {
                        string ticketId = parts[0];
                        string scheduleId = parts[1];
                        int seatNumber = int.Parse(parts[2]);
                        DateTime purchaseTime = DateTime.Parse(parts[3]);
                        double price = double.Parse(parts[4]);

                        // Find the schedule for this ticket
                        Schedule schedule = _schedules.Find(s => s.GetScheduleId() == scheduleId);
                        if (schedule != null)
                        {
                            // Create ticket object using the new constructor
                            Ticket ticket = new Ticket(ticketId, schedule, seatNumber, purchaseTime, price);
                            _tickets.Add(ticket);
                        }
                    }
                }
            }

            Console.WriteLine($"Loaded: {_buses.Count} buses, {_routes.Count} routes, {_schedules.Count} schedules, {_tickets.Count} tickets");
        }

        private void SaveSystemData()
        {
            Console.WriteLine("\nSaving system data...");

            List<string> busData = new List<string>();
            foreach (Bus bus in _buses)
            {
                string line = $"{bus.GetBusId()},{bus.GetLicensePlate()},{bus.GetTotalSeats()}," +
                            $"{bus.IsAvailable()},{bus.GetDriverName()}";
                busData.Add(line);
            }
            _dataManager.SaveBuses(busData);

            List<string> routeData = new List<string>();
            foreach (Route route in _routes)
            {
                string stops = string.Join(",", route.GetStops());
                string line = $"{route.GetRouteId()},{route.GetDestination()}";
                if (!string.IsNullOrEmpty(stops))
                    line += $",{stops}";
                routeData.Add(line);
            }
            _dataManager.SaveRoutes(routeData);

            List<string> scheduleData = new List<string>();
            foreach (Schedule schedule in _schedules)
            {
                List<int> bookedSeats = schedule.GetBookedSeats();
                string bookedSeatsStr = string.Join("|", bookedSeats);

                string line = $"{schedule.GetScheduleId()},{schedule.GetBus().GetBusId()}," +
                            $"{schedule.GetRoute().GetRouteId()},{schedule.GetDepartureTime()}," +
                            $"{bookedSeatsStr}";
                scheduleData.Add(line);
            }
            _dataManager.SaveSchedules(scheduleData);

            List<string> ticketData = new List<string>();
            foreach (Ticket ticket in _tickets)
            {
                string scheduleId = "";
                foreach (Schedule schedule in _schedules)
                {
                    if (schedule.GetBookedSeats().Contains(ticket.GetSeatNumber()))
                    {
                        scheduleId = schedule.GetScheduleId();
                        break;
                    }
                }

                string line = $"{ticket.GetTicketId()},{scheduleId},{ticket.GetSeatNumber()}," +
                            $"{ticket.GetPurchaseTime()},{ticket.GetPrice()}";
                ticketData.Add(line);
            }
            _dataManager.SaveTickets(ticketData);

            Console.WriteLine("Data saved successfully!");
        }

        private void RunMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== BUS TERMINAL MAIN MENU ===");
                Console.WriteLine("1. View Departures");
                Console.WriteLine("2. Book Ticket");
                Console.WriteLine("3. Add Schedule");
                Console.WriteLine("4. View System Info");
                Console.WriteLine("5. Save and Exit");
                Console.Write("\nEnter choice (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewDepartures();
                        break;
                    case "2":
                        BookTicket();
                        break;
                    case "3":
                        AddSchedule();
                        break;
                    case "4":
                        ViewSystemInfo();
                        break;
                    case "5":  // Save and Exit program
                        SaveSystemData();
                        Console.WriteLine("\nThank you for using Bus Terminal System!");
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        return;  // Exit the while loop and method
                    default:
                        Console.WriteLine("\nInvalid choice! Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Show all bus departures
        private void ViewDepartures()
        {
            Console.Clear();
            Console.WriteLine("=== ALL SCHEDULES ===\n");

            if (_schedules.Count == 0)
            {
                Console.WriteLine("No schedules found in the system.");
                WaitForKey();
                return;
            }

            // Sort schedules by departure time
            List<Schedule> sortedSchedules = new List<Schedule>(_schedules);

            for (int i = 0; i < sortedSchedules.Count - 1; i++)
            {
                for (int j = i + 1; j < sortedSchedules.Count; j++)
                {
                    if (sortedSchedules[i].GetDepartureTime() > sortedSchedules[j].GetDepartureTime())
                    {
                        Schedule temp = sortedSchedules[i];
                        sortedSchedules[i] = sortedSchedules[j];
                        sortedSchedules[j] = temp;
                    }
                }
            }

            Console.WriteLine("Time     | Date       | Bus   | Destination     | Available Seats");
            Console.WriteLine("---------|------------|-------|-----------------|----------------");

            foreach (Schedule schedule in sortedSchedules)
            {
                Console.WriteLine($"{schedule.GetDepartureTime():HH:mm}    | " +
                                $"{schedule.GetDepartureTime():MM/dd/yyyy} | " +
                                $"{schedule.GetBus().GetBusId(),-5} | " +
                                $"{schedule.GetRoute().GetDestination(),-15} | " +
                                $"{schedule.GetAvailableSeatCount()}");
            }

            Console.WriteLine($"\nTotal schedules: {_schedules.Count}");
            WaitForKey();
        }

        // Book a ticket for a specific schedule and seat
        private void BookTicket()
        {
            Console.Clear();
            Console.WriteLine("=== BOOK A TICKET ===\n");

            // Find schedules with available seats
            List<Schedule> availableSchedules = new List<Schedule>();

            foreach (Schedule schedule in _schedules)
            {
                if (schedule.GetAvailableSeatCount() > 0)
                {
                    availableSchedules.Add(schedule);
                }
            }

            // Check if any schedules are available
            if (availableSchedules.Count == 0)
            {
                Console.WriteLine("No schedules available for booking.");
                WaitForKey();
                return;  // Exit method early
            }

            // Display available schedules
            Console.WriteLine("Available Schedules:");
            Console.WriteLine("ID    | Destination     | Date       | Time  | Seats");
            Console.WriteLine("------|-----------------|------------|-------|------");

            foreach (Schedule schedule in availableSchedules)
            {
                Console.WriteLine($"{schedule.GetScheduleId(),-5} | " +
                                $"{schedule.GetRoute().GetDestination(),-15} | " +
                                $"{schedule.GetDepartureTime():MM/dd}      | " +
                                $"{schedule.GetDepartureTime():HH:mm} | " +
                                $"{schedule.GetAvailableSeatCount(),-5}");
            }

            // Get schedule ID from user
            Console.Write("\nEnter Schedule ID: ");
            string scheduleId = Console.ReadLine().ToUpper();

            // Find the selected schedule
            Schedule selectedSchedule = null;
            foreach (Schedule schedule in _schedules)
            {
                if (schedule.GetScheduleId().ToUpper() == scheduleId)
                {
                    selectedSchedule = schedule;  // Found it!
                    break;  // Stop searching
                }
            }

            // Check if schedule was found
            if (selectedSchedule == null)
            {
                // Show available IDs to help user
                Console.Write("Schedule not found! Available IDs: ");
                foreach (Schedule schedule in _schedules)
                {
                    Console.Write($"{schedule.GetScheduleId()} ");
                }
                Console.WriteLine();
                WaitForKey();
                return;
            }

            // Get list of available seats for this schedule
            List<int> availableSeats = selectedSchedule.GetAvailableSeats();

            // Check if any seats are available
            if (availableSeats.Count == 0)
            {
                Console.WriteLine("No seats available for this schedule!");
                WaitForKey();
                return;
            }

            // Display available seats
            Console.WriteLine($"\nAvailable seats for {selectedSchedule.GetScheduleId()}:");

            // Display seats in rows of 10 for better readability
            int seatsPerRow = 10;  // Show 10 seats per row
            for (int i = 0; i < availableSeats.Count; i++)
            {
                Console.Write($"{availableSeats[i],3} ");  // Print seat number
                // Start new line after every 10 seats
                if ((i + 1) % seatsPerRow == 0) Console.WriteLine();
            }
            // If last row wasn't complete, add new line
            if (availableSeats.Count % seatsPerRow != 0) Console.WriteLine();

            // Get seat selection from user
            Console.Write("\nChoose seat number (1-50): ");
            string seatInput = Console.ReadLine();

            // Validate seat input is a number
            if (!int.TryParse(seatInput, out int seatNumber))
            {
                Console.WriteLine("Invalid seat number!");
                WaitForKey();
                return;
            }

            // Validate seat number is in valid range (1-50)
            if (seatNumber < 1 || seatNumber > 50)
            {
                Console.WriteLine("Seat number must be between 1 and 50!");
                WaitForKey();
                return;
            }

            // Try to book the seat
            if (selectedSchedule.BookSeat(seatNumber))
            {
                // Calculate ticket price: $50 base + $5 per stop
                double ticketPrice = 50.00 + (selectedSchedule.GetRoute().GetStops().Count * 5.00);

                // Create new ticket object
                Ticket newTicket = new Ticket(selectedSchedule, seatNumber, ticketPrice);

                // Add ticket to tickets list
                _tickets.Add(newTicket);

                // Print the ticket (like POS system)
                newTicket.PrintTicket();
                // Print the receipt (like POS system)
                newTicket.PrintReceipt();

                Console.WriteLine("Booking successful!");
            }
            else
            {
                Console.WriteLine("Failed to book seat. Seat may be already taken.");
            }

            WaitForKey();
        }

        // Add a new schedule (admin function)
        private void AddSchedule()
        {
            Console.Clear();
            Console.WriteLine("=== ADD NEW SCHEDULE ===\n");

            // Show ALL buses with their status
            Console.WriteLine("All Buses:");
            Console.WriteLine("ID    | Driver          | Status    | Seats");
            Console.WriteLine("------|-----------------|-----------|------");

            foreach (Bus bus in _buses)
            {
                string status = bus.IsAvailable() ? "Available" : "In Service";
                Console.WriteLine($"{bus.GetBusId(),-5} | " +
                                $"{bus.GetDriverName(),-15} | " +
                                $"{status,-10} | " +
                                $"{bus.GetTotalSeats(),-5}");
            }

            // Get bus selection from user
            Console.Write("\nEnter Bus ID: ");
            string busId = Console.ReadLine().ToUpper();

            // Find selected bus
            Bus selectedBus = null;
            foreach (Bus bus in _buses)
            {
                if (bus.GetBusId().ToUpper() == busId)
                {
                    selectedBus = bus;
                    break;
                }
            }

            // Check if bus was found
            if (selectedBus == null)
            {
                // Show available bus IDs
                Console.Write("Bus not found! Available IDs: ");
                foreach (Bus bus in _buses)
                {
                    Console.Write($"{bus.GetBusId()} ");
                }
                Console.WriteLine();
                WaitForKey();
                return;
            }

            // Check if bus is available (not in service)
            if (!selectedBus.IsAvailable())
            {
                Console.WriteLine($"Bus {busId} is already in service!");
                WaitForKey();
                return;
            }

            // Show available routes
            Console.WriteLine("\nAvailable Routes:");
            foreach (Route route in _routes)
            {
                // Show stops if route has them
                string stops = route.GetStops().Count > 0 ?
                    $" (via {string.Join(", ", route.GetStops())})" : "";
                Console.WriteLine($"{route.GetRouteId()}: {route.GetDestination()}{stops}");
            }

            // Get route selection
            Console.Write("\nEnter Route ID: ");
            string routeId = Console.ReadLine().ToUpper();

            // Find selected route
            Route selectedRoute = null;
            foreach (Route route in _routes)
            {
                if (route.GetRouteId().ToUpper() == routeId)
                {
                    selectedRoute = route;
                    break;
                }
            }

            // Check if route was found
            if (selectedRoute == null)
            {
                // Show available route IDs
                Console.Write("Route not found! Available IDs: ");
                foreach (Route route in _routes)
                {
                    Console.Write($"{route.GetRouteId()} ");
                }
                Console.WriteLine();
                WaitForKey();
                return;
            }

            // Get departure date and time
            Console.WriteLine("\nEnter departure date and time:");
            Console.Write("Date (yyyy-MM-dd): ");
            string dateInput = Console.ReadLine();
            Console.Write("Time (HH:mm): ");
            string timeInput = Console.ReadLine();

            if (!DateTime.TryParse($"{dateInput} {timeInput}", out DateTime departureTime))
            {
                Console.WriteLine("Invalid date/time format!");
                WaitForKey();
                return;
            }

            // Generate new schedule ID (SCH15, SCH16, etc.)
            string newScheduleId = "SCH" + (_schedules.Count + 1).ToString("00");

            // Create new schedule object
            Schedule newSchedule = new Schedule(newScheduleId, selectedBus, selectedRoute, departureTime);

            // Check for schedule conflicts
            bool conflictFound = false;
            foreach (Schedule existingSchedule in _schedules)
            {
                // Check if new schedule conflicts with existing one
                if (newSchedule.HasConflict(existingSchedule))
                {
                    conflictFound = true;
                    Console.WriteLine("\n=== SCHEDULE CONFLICT DETECTED ===");
                    Console.WriteLine($"Bus {busId} already has a schedule:");
                    Console.WriteLine($"- ID: {existingSchedule.GetScheduleId()}");
                    Console.WriteLine($"- To: {existingSchedule.GetRoute().GetDestination()}");
                    Console.WriteLine($"- Time: {existingSchedule.GetDepartureTime():yyyy-MM-dd HH:mm}");
                    break;  // Stop checking after first conflict
                }
            }

            // If no conflicts found, add the new schedule
            if (!conflictFound)
            {
                _schedules.Add(newSchedule);  // Add to schedules list
                Console.WriteLine($"\nSchedule created successfully!");
                Console.WriteLine($"ID: {newScheduleId}");
                Console.WriteLine($"Bus: {selectedBus.GetBusId()} ({selectedBus.GetDriverName()})");
                Console.WriteLine($"Route: {selectedRoute.GetDestination()}");
                Console.WriteLine($"Departure: {departureTime:yyyy-MM-dd HH:mm}");
                Console.WriteLine($"Arrival: ~{departureTime.AddHours(3):HH:mm}");
                Console.WriteLine($"Seats: 50 available");
            }
            else
            {
                Console.WriteLine("\nSchedule not created due to conflict.");
            }

            WaitForKey();
        }

        // Show system information and statistics
        private void ViewSystemInfo()
        {
            Console.Clear();
            Console.WriteLine("=== SYSTEM INFORMATION ===\n");

            // Calculate total buses
            int totalBuses = _buses.Count;

            // Count available buses
            int availableBuses = 0;
            foreach (Bus bus in _buses)
            {
                if (bus.IsAvailable())
                    availableBuses++;
            }
            // Calculate active buses
            int activeBuses = totalBuses - availableBuses;

            // Count routes and schedules
            int totalRoutes = _routes.Count;
            int totalSchedules = _schedules.Count;

            // Count tickets sold
            int totalTickets = _tickets.Count;

            // Calculate total revenue
            double totalRevenue = 0.0;
            foreach (Ticket ticket in _tickets)
            {
                totalRevenue += ticket.GetPrice();  // Add each ticket's price
            }

            // Display system statistics
            Console.WriteLine($"Total Buses:        {totalBuses}");
            Console.WriteLine($"  • Available:      {availableBuses}");
            Console.WriteLine($"  • In Service:     {activeBuses}");
            Console.WriteLine($"Total Routes:       {totalRoutes}");
            Console.WriteLine($"Total Schedules:    {totalSchedules}");
            Console.WriteLine($"Tickets Sold:       {totalTickets}");
            Console.WriteLine($"Total Revenue:      ${totalRevenue:F2}");

            // ========== UPDATED SECTION STARTS HERE ==========
            // Show bus details with status
            Console.WriteLine("\n--- Bus Details ---");
            int availableCount = 0;
            int inServiceCount = 0;

            foreach (Bus bus in _buses)
            {
                string status = bus.IsAvailable() ? "Available" : "In Service";
                string availabilityNote = bus.IsAvailable() ? " ✓ CAN BE ASSIGNED" : "";

                if (bus.IsAvailable()) availableCount++;
                else inServiceCount++;

                Console.WriteLine($"{bus.GetBusId()}: {bus.GetDriverName()} " +
                                $"({bus.GetTotalSeats()} seats) - {status}{availabilityNote}");
            }

            Console.WriteLine($"\nSummary: {availableCount} available, {inServiceCount} in service");
            // ========== UPDATED SECTION ENDS HERE ==========

            // Show route details with stop count
            Console.WriteLine("\n--- Route Details ---");
            foreach (Route route in _routes)
            {
                Console.WriteLine($"{route.GetRouteId()}: {route.GetDestination()} " +
                                $"({route.GetStops().Count} stops)");
            }

            // Show some schedules
            Console.WriteLine("\n--- Recent Schedules ---");
            int count = 0;
            foreach (Schedule schedule in _schedules)
            {
                Console.WriteLine($"{schedule.GetScheduleId()}: {schedule.GetBus().GetBusId()} " +
                                $"to {schedule.GetRoute().GetDestination()} at " +
                                $"{schedule.GetDepartureTime():MM/dd HH:mm} " +
                                $"({schedule.GetAvailableSeatCount()} seats available)");
                count++;
                if (count >= 5) break;  // Show only first 5
            }
            if (count == 0)
                Console.WriteLine("No schedules found.");

            WaitForKey();
        }

        // Helper method to wait for user input
        private void WaitForKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();  // Wait for any key press
        }
    }
}