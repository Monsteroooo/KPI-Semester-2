using System;
using System.Collections.Generic;

namespace TravelAgency.Models
{
    public enum UserRole { Client, Agent }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string FullName { get; set; } = "";
        public UserRole Role { get; set; }
    }

    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; } = "";
        public string Origin { get; set; } = "";
        public string Destination { get; set; } = "";
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
    }

    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = "Pending Payment";
    }

    public class AppData
    {
        public List<User> Users { get; set; } = new();
        public List<Flight> Flights { get; set; } = new();
        public List<Booking> Bookings { get; set; } = new();
    }
}
