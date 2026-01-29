using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FINALS
{
    internal class Ticket
    {
        private string _ticketId;        
        private Schedule _bookedSchedule; 
        private int _seatNumber;         
        private DateTime _purchaseTime;  
        private double _price;           

        public Ticket(Schedule schedule, int seatNum, double price)
        {
            _ticketId = "TKT:" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
            _bookedSchedule = schedule;  
            _seatNumber = seatNum;       
            _purchaseTime = DateTime.Now; 
            _price = price;              
        }

        public Ticket(string ticketId, Schedule schedule, int seatNum, DateTime purchaseTime, double price)
        {
            _ticketId = ticketId;
            _bookedSchedule = schedule;
            _seatNumber = seatNum;
            _purchaseTime = purchaseTime;
            _price = price;
        }

        public string GetTicketId() { return _ticketId; }
        public int GetSeatNumber() { return _seatNumber; }
        public double GetPrice() { return _price; }
        public DateTime GetPurchaseTime() { return _purchaseTime; }

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