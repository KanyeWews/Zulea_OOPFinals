using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class TerminalSystem
    {
        // Store all buses in the terminal
        private List<Bus> _buses = new List<Bus>();
        // Store all available routes
        private List<Route> _routes = new List<Route>();
        // Store all scheduled trips
        private List<Schedule> _schedules = new List<Schedule>();
        // Store all tickets sold today
        private List<Ticket> _tickets = new List<Ticket>();

        // Constructor - runs when program starts
        public TerminalSystem()
        {
            Console.WriteLine("=== BUS TERMINAL SYSTEM ===\n");
            CreateSampleData();  // Create initial test data
            RunMainMenu();       // Show main menu to user
        }

        // Create sample data for testing
        private void CreateSampleData()
        {
            Console.WriteLine("Creating sample data...\n");

            // Create first bus object
            Bus bus1 = new Bus("BUS01", "ABC-123", 50);
            // Create second bus object
            Bus bus2 = new Bus("BUS02", "XYZ-789", 45);
            // Assign driver to first bus
            bus1.AssignDriver("John Smith");
            // Assign driver to second bus
            bus2.AssignDriver("Maria Garcia");

            // Add buses to the list
            _buses.Add(bus1);
            _buses.Add(bus2);

            // Create first route (Los Angeles with stops)
            Route route1 = new Route("R01", "Los Angeles");
            route1.AddStop("San Diego");      // Add first stop
            route1.AddStop("Santa Barbara");  // Add second stop

            // Create second route (San Francisco with stops)
            Route route2 = new Route("R02", "San Francisco");
            route2.AddStop("Sacramento");  // Add first stop
            route2.AddStop("San Jose");    // Add second stop

            // Add routes to the list
            _routes.Add(route1);
            _routes.Add(route2);

            // **FIX: Use FUTURE dates (tomorrow) instead of today**
            DateTime today = DateTime.Today;        // Get today's date (midnight)
            DateTime tomorrow = today.AddDays(1);   // Get tomorrow's date

            // Schedule 1: Bus1 on Route1, TOMORROW at 8 AM (was: today at 8 AM)
            Schedule schedule1 = new Schedule("SCH01", bus1, route1, tomorrow.AddHours(8));

            // Schedule 2: Bus2 on Route2, TOMORROW at 10 AM (was: today at 10 AM)
            Schedule schedule2 = new Schedule("SCH02", bus2, route2, tomorrow.AddHours(10));

            // Schedule 3: Bus1 on Route1, TOMORROW at 3 PM (was: today at 3 PM)
            Schedule schedule3 = new Schedule("SCH03", bus1, route1, tomorrow.AddHours(15));

            // **NEW: Added 4th schedule for day after tomorrow**
            Schedule schedule4 = new Schedule("SCH04", bus2, route1, today.AddDays(2).AddHours(9));

            // Add all schedules to the list
            _schedules.Add(schedule1);
            _schedules.Add(schedule2);
            _schedules.Add(schedule3);
            _schedules.Add(schedule4);

            // **FIX: Book seats on multiple schedules (not just schedule1)**
            schedule1.BookSeat(5);   // Book seat 5 on schedule1
            schedule1.BookSeat(12);  // Book seat 12 on schedule1
            schedule1.BookSeat(23);  // Book seat 23 on schedule1

            // **NEW: Also book seats on other schedules**
            schedule2.BookSeat(8);   // Book seat 8 on schedule2
            schedule2.BookSeat(15);  // Book seat 15 on schedule2
            schedule3.BookSeat(30);  // Book seat 30 on schedule3

            Console.WriteLine("Sample data created successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();  // Wait for user to press any key
        }

        // Main menu - shows options to user
        private void RunMainMenu()
        {
            while (true)  // Infinite loop until user chooses to exit
            {
                Console.Clear();  // Clear the console screen
                Console.WriteLine("=== BUS TERMINAL MAIN MENU ===");
                Console.WriteLine("1. View Departures");
                Console.WriteLine("2. Book Ticket");
                Console.WriteLine("3. Add Schedule");
                Console.WriteLine("4. View System Info");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter choice (1-5): ");

                // Get user's choice
                string choice = Console.ReadLine();

                // Process user's choice using switch statement
                switch (choice)
                {
                    case "1":
                        ViewDepartures();  // Show upcoming bus departures
                        break;
                    case "2":
                        BookTicket();      // Start ticket booking process
                        break;
                    case "3":
                        AddSchedule();     // Allow adding new schedule
                        break;
                    case "4":
                        ViewSystemInfo();  // Show system statistics
                        break;
                    case "5":  // Exit program
                        Console.WriteLine("\nThank you for using Bus Terminal System!");
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        return;  // Exit the while loop and method
                    default:  // Invalid input
                        Console.WriteLine("\nInvalid choice! Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Show all upcoming bus departures
        private void ViewDepartures()
        {
            Console.Clear();
            Console.WriteLine("=== UPCOMING DEPARTURES ===\n");

            DateTime now = DateTime.Now;  // Get current date and time

            // Create empty list for upcoming schedules
            List<Schedule> upcomingSchedules = new List<Schedule>();

            // Loop through ALL schedules to find upcoming ones
            foreach (Schedule schedule in _schedules)
            {
                // Check if departure time is in the FUTURE
                if (schedule.GetDepartureTime() > now)
                {
                    upcomingSchedules.Add(schedule);  // Add to upcoming list
                }
            }

            // **FIX: Sort schedules by departure time (Bubble Sort - no LINQ)**
            // Bubble sort compares each pair and swaps if in wrong order
            for (int i = 0; i < upcomingSchedules.Count - 1; i++)
            {
                for (int j = i + 1; j < upcomingSchedules.Count; j++)
                {
                    // If schedule[i] departs LATER than schedule[j]
                    if (upcomingSchedules[i].GetDepartureTime() > upcomingSchedules[j].GetDepartureTime())
                    {
                        // Swap the two schedules
                        Schedule temp = upcomingSchedules[i];
                        upcomingSchedules[i] = upcomingSchedules[j];
                        upcomingSchedules[j] = temp;
                    }
                }
            }

            // Check if any upcoming schedules were found
            if (upcomingSchedules.Count == 0)
            {
                Console.WriteLine("No upcoming departures found.");
                // **FIX: Added helpful message**
                Console.WriteLine("\nNote: All sample schedules are for tomorrow.");
            }
            else
            {
                // **FIX: Improved table header with DATE column**
                Console.WriteLine("ID    | Bus   | Destination     | Date       | Time  | Seats");
                Console.WriteLine("------|-------|-----------------|------------|-------|------");

                // Display each upcoming schedule
                foreach (Schedule schedule in upcomingSchedules)
                {
                    // Format: SCH01 | BUS01 | Los Angeles | 12/16 | 08:00 | 47
                    Console.WriteLine($"{schedule.GetScheduleId(),-5} | " +
                                    $"{schedule.GetBus().GetBusId(),-5} | " +
                                    $"{schedule.GetRoute().GetDestination(),-15} | " +
                                    $"{schedule.GetDepartureTime():MM/dd}      | " +  // Show date (month/day)
                                    $"{schedule.GetDepartureTime():HH:mm} | " +  // Show time (hour:minute)
                                    $"{schedule.GetAvailableSeatCount(),-5}");   // Show available seats
                }
            }

            WaitForKey();  // Wait for user to press a key
        }

        // Book a ticket for a specific schedule and seat
        private void BookTicket()
        {
            Console.Clear();
            Console.WriteLine("=== BOOK A TICKET ===\n");

            DateTime now = DateTime.Now;  // Current time

            // Find schedules available for booking
            List<Schedule> availableSchedules = new List<Schedule>();

            foreach (Schedule schedule in _schedules)
            {
                // Schedule must be in FUTURE AND have available seats
                if (schedule.GetDepartureTime() > now && schedule.GetAvailableSeatCount() > 0)
                {
                    availableSchedules.Add(schedule);
                }
            }

            // Check if any schedules are available
            if (availableSchedules.Count == 0)
            {
                Console.WriteLine("No schedules available for booking.");
                // **FIX: Added helpful message**
                Console.WriteLine("Note: Sample schedules are for tomorrow.");
                WaitForKey();
                return;  // Exit method early
            }

            // **FIX: Improved display format with table**
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
            // **FIX: Convert to uppercase for case-insensitive comparison**
            string scheduleId = Console.ReadLine().ToUpper();

            // Find the selected schedule
            Schedule selectedSchedule = null;
            foreach (Schedule schedule in _schedules)
            {
                // **FIX: Compare uppercase versions (case-insensitive)**
                if (schedule.GetScheduleId().ToUpper() == scheduleId)
                {
                    selectedSchedule = schedule;  // Found it!
                    break;  // Stop searching
                }
            }

            // Check if schedule was found
            if (selectedSchedule == null)
            {
                // **FIX: Show available IDs to help user**
                Console.WriteLine("Schedule not found! Available IDs: SCH01, SCH02, SCH03, SCH04");
                WaitForKey();
                return;
            }

            // Check if schedule hasn't departed yet
            if (selectedSchedule.GetDepartureTime() < now)
            {
                Console.WriteLine("This schedule has already departed!");
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

                // Add ticket to today's tickets list
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

            // **FIX: Show ALL buses with their status**
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
            // **FIX: Convert to uppercase for case-insensitive comparison**
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
                // **FIX: Show available bus IDs**
                Console.WriteLine("Bus not found! Available IDs: BUS01, BUS02");
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
                // **FIX: Show available route IDs**
                Console.WriteLine("Route not found! Available IDs: R01, R02");
                WaitForKey();
                return;
            }

            // **FIX: Show current time for reference**
            Console.WriteLine($"\nCurrent date/time: {DateTime.Now:yyyy-MM-dd HH:mm}");
            Console.WriteLine("Note: Enter a FUTURE date/time");
            Console.Write("Enter departure time (yyyy-MM-dd HH:mm): ");
            string timeInput = Console.ReadLine();

            // Validate time input format
            if (!DateTime.TryParse(timeInput, out DateTime departureTime))
            {
                Console.WriteLine("Invalid date/time format!");
                WaitForKey();
                return;
            }

            // Check if time is in the future
            if (departureTime < DateTime.Now)
            {
                Console.WriteLine("Cannot schedule in the past!");
                WaitForKey();
                return;
            }

            // Generate new schedule ID (SCH05, SCH06, etc.)
            string newScheduleId = "SCH" + (_schedules.Count + 1).ToString("00");

            // Create new schedule object
            Schedule newSchedule = new Schedule(newScheduleId, selectedBus, selectedRoute, departureTime);

            // **KEY FEATURE: Check for schedule conflicts**
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

            DateTime now = DateTime.Now;  // Current time

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

            // Count upcoming and completed schedules
            int upcomingSchedules = 0;
            int completedSchedules = 0;
            foreach (Schedule schedule in _schedules)
            {
                if (schedule.GetDepartureTime() > now)
                    upcomingSchedules++;
                else
                    completedSchedules++;
            }

            // Count tickets sold today
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
            Console.WriteLine($"  • Upcoming:       {upcomingSchedules}");
            Console.WriteLine($"  • Completed:      {completedSchedules}");
            Console.WriteLine($"Tickets Sold Today: {totalTickets}");
            Console.WriteLine($"Today's Revenue:    ${totalRevenue:F2}");

            // **FIX: Show bus details with status**
            Console.WriteLine("\n--- Bus Details ---");
            foreach (Bus bus in _buses)
            {
                string status = bus.IsAvailable() ? "Available" : "In Service";
                Console.WriteLine($"{bus.GetBusId()}: {bus.GetDriverName()} " +
                                $"({bus.GetTotalSeats()} seats) - {status}");
            }

            // **FIX: Show route details with stop count**
            Console.WriteLine("\n--- Route Details ---");
            foreach (Route route in _routes)
            {
                Console.WriteLine($"{route.GetRouteId()}: {route.GetDestination()} " +
                                $"({route.GetStops().Count} stops)");
            }

            // **NEW: Show some upcoming schedules**
            Console.WriteLine("\n--- Upcoming Schedules ---");
            int count = 0;
            foreach (Schedule schedule in _schedules)
            {
                if (schedule.GetDepartureTime() > now)  // Only show future schedules
                {
                    Console.WriteLine($"{schedule.GetScheduleId()}: {schedule.GetBus().GetBusId()} " +
                                    $"to {schedule.GetRoute().GetDestination()} at " +
                                    $"{schedule.GetDepartureTime():MM/dd HH:mm} " +
                                    $"({schedule.GetAvailableSeatCount()} seats available)");
                    count++;
                    if (count >= 5) break;  // Show only first 5
                }
            }
            if (count == 0)  // If no upcoming schedules
                Console.WriteLine("No upcoming schedules.");

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