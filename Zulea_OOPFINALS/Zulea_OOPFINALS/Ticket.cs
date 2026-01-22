using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class Ticket  // Represents a purchased ticket
    {
        // Private fields
        private string _ticketId;        // Unique ticket ID
        private Schedule _bookedSchedule; // Which schedule was booked
        private int _seatNumber;         // Which seat
        private DateTime _purchaseTime;  // When purchased
        private double _price;           // Ticket price

        // Constructor
        public Ticket(Schedule schedule, int seatNum, double price)
        {
            // Generate unique ticket ID (first 5 chars of GUID)
            _ticketId = "TKT" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
            _bookedSchedule = schedule;  // Store schedule
            _seatNumber = seatNum;       // Store seat number
            _purchaseTime = DateTime.Now; // Current time
            _price = price;              // Store price
        }

        // Getter methods
        public string GetTicketId() { return _ticketId; }
        public int GetSeatNumber() { return _seatNumber; }
        public double GetPrice() { return _price; }
        public DateTime GetPurchaseTime() { return _purchaseTime; }

        // Print ticket - like POS system
        public void PrintTicket()
        {
            Console.WriteLine("\n========= BUS TICKET =========");
            Console.WriteLine($"Ticket#: {_ticketId}");
            Console.WriteLine($"Seat:    {_seatNumber}");
            Console.WriteLine($"Bus:     {_bookedSchedule.GetBus().GetBusId()}");
            Console.WriteLine($"Route:   {_bookedSchedule.GetRoute().GetDestination()}");
            Console.WriteLine($"Time:    {_bookedSchedule.GetDepartureTime():HH:mm}");
            Console.WriteLine($"Price:   ${_price:F2}");
            Console.WriteLine($"Date:    {_purchaseTime:MM/dd/yyyy}");
            Console.WriteLine("==============================\n");
        }

        // Print receipt - like POS system
        public void PrintReceipt()
        {
            Console.WriteLine("\n========= RECEIPT =========");
            Console.WriteLine($"Receipt#: RCP-{_ticketId}");
            Console.WriteLine($"Ticket:   {_ticketId}");
            Console.WriteLine($"Seat:     {_seatNumber}");
            Console.WriteLine($"Amount:   ${_price:F2}");
            Console.WriteLine($"Time:     {_purchaseTime:HH:mm:ss}");
            Console.WriteLine("==========================\n");
        }
    }
}