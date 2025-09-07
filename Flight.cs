using System;
using System.Collections.Generic;
using System.Data;

namespace Traveling_Services
{
    public enum TravelClass
    {
        None = 0,
        Economy,
        Business,
        FirstClass
    }

    public enum Country
    {
        None = 0,
        Egypt,
        SaudiArabia,
        UnitedArabEmirates,
        Qatar,
        Kuwait,
        Bahrain,
        Oman,
        Jordan,
        Lebanon,
        Syria,
        Iraq,
        Yemen,
        Palestine,
        Sudan,
        Libya,
        Morocco,
        Algeria,
        Tunisia,
        Mauritania,
        Somalia,
        Djibouti,
        Comoros,
        Turkey,
        Iran,
        Pakistan,
        Afghanistan,
        India,
        Bangladesh,
        SriLanka,
        Nepal,
        China,
        Japan,
        SouthKorea,
        NorthKorea,
        Malaysia,
        Indonesia,
        Philippines,
        Thailand,
        Vietnam,
        Singapore,
        Myanmar,
        Cambodia,
        Laos,
        Australia,
        NewZealand,
        USA,
        Canada,
        Mexico,
        Brazil,
        Argentina,
        Chile
    }

    // Session class حفظ المستخدم الحالي
    public static class Session
    {
        public static string CurrentUser { get; set; }
        public static string CurrentPassword { get; set; }
        public static bool LoggedIn { get; set; } = false;
    }

    // Flight يرث من Booking + Person
    class Flight : Booking
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ServiceType Role { get; set; }

        public Country origin { get; set; }
        public Country destination { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }
        public TravelClass travelClass { get; set; }
        public int SeatNumber { get; set; }  
        public long price { get; set; }

        protected Random seatnumber = new Random();

        public void Logout()
        {
            if (Session.LoggedIn)
            {
                Session.CurrentUser = null;
                Session.CurrentPassword = null;
                Session.LoggedIn = false;
                Console.WriteLine(" Logout Successful");
            }
            else
            {
                Console.WriteLine(" You are not logged in.");
            }
        }
    }

    // FlightBooking يرث من Flight
    class FlightBooking : Flight
    {
        /*
        public FlightBooking() : base()
        {
            if (Role != ServiceType.Customer && Role != ServiceType.SystemAdmin && Role != ServiceType.AirlineAdmin)
            {
                Console.WriteLine("Only customers or admins can make flight bookings.");
            }
        }
        */
        public void confirmBooking(List<FlightBooking> flightBookings)
        {
            if (Primary.CurrentUser != null && Primary.CurrentUser.LoggedIn)
            {
                FlightBooking booking = new FlightBooking();

                booking.Username = Primary.CurrentUser.Username;
                booking.Password = Primary.CurrentUser.Password;
                foreach (Country c in Enum.GetValues(typeof(Country)))
                {

                    if (c != Country.None) // علشان ما يعرضش 0
                        Console.WriteLine($"{(int)c}. {c}");
                }

                booking.origin = (Country)ExceptionHandling.ValidInt("Enter your origin: ", 1, 50);
                booking.destination = (Country)ExceptionHandling.ValidInt("Enter your destination: ", 1, 50);
                booking.travelClass = (TravelClass)ExceptionHandling.ValidInt("Enter Travel Class (1.Economy, 2.Business, 3.FirstClass): ", 1, 3);

                booking.departureTime = DateTime.Now.AddDays(7);
                booking.arrivalTime = booking.departureTime.AddHours(5);

                if (booking.travelClass == TravelClass.Business)
                {
                    booking.price = 5000;
                    booking.SeatNumber = seatnumber.Next(270, 500);
                }
                else if (booking.travelClass == TravelClass.FirstClass)
                {
                    booking.price = 10000;
                    booking.SeatNumber = seatnumber.Next(51, 270);
                }
                else
                {
                    booking.price = 980;
                    booking.SeatNumber = seatnumber.Next(1, 50);
                }

                flightBookings.Add(booking);
                booking.PrintTicket();

                Console.WriteLine(" Booking confirmed successfully!");
            }
            else
            {
                Console.WriteLine(" You must login first to confirm booking.");
            }
        }

        public void PrintTicket()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('*', 50));
            Console.WriteLine($"{"",15}  Flight Ticket ");
            Console.WriteLine(new string('*', 50));
            Console.ResetColor();

            Console.WriteLine($"Passenger:      {Username}");
            Console.WriteLine($"Role:           {Primary.CurrentUser.Role}");
            Console.WriteLine($"From:           {origin}");
            Console.WriteLine($"To:             {destination}");
            Console.WriteLine($"Departure:      {departureTime}");
            Console.WriteLine($"Arrival:        {arrivalTime}");
            Console.WriteLine($"Duration:       {(arrivalTime - departureTime).TotalHours} hours");
            Console.WriteLine($"Travel Class:   {travelClass}");
            Console.WriteLine($"Seat Number:    {SeatNumber}");
            Console.WriteLine($"Price:          {price:C}");
            Console.WriteLine(new string('*', 50));
        }

        public void ViewBookingDetails()
        {
            if (Session.LoggedIn)
            {
                Console.WriteLine("Booking Details:");
                Console.WriteLine($"Booking ID: {BookingID}");
                Console.WriteLine($"Customer Name: {Username}");
                Console.WriteLine($"Origin: {origin}");
                Console.WriteLine($"Destination: {destination}");
                Console.WriteLine($"Departure Time: {departureTime}");
                Console.WriteLine($"Arrival Time: {arrivalTime}");
                Console.WriteLine($"Travel Class: {travelClass}");
                Console.WriteLine($"Seat Number: {SeatNumber}");
                Console.WriteLine($"Price: {price:c}");
            }
            else
            {
                Console.WriteLine(" You are not authorized to see this info!");
            }
        }

        public void cancelBooking(List<FlightBooking> flightBookings)
        {
            if (!Session.LoggedIn)
            {
                Console.WriteLine(" You must login first!");
                return;
            }

            string password = ExceptionHandling.NonEmptyString("Enter Password: ");

            for (int i = 0; i < flightBookings.Count; i++)
            {
                if (flightBookings[i].Password == password && flightBookings[i].Username == Session.CurrentUser)
                {
                    flightBookings.RemoveAt(i);
                    Console.WriteLine(" Booking Cancelled Successfully");
                    return;
                }
            }
            Console.WriteLine(" Invalid Password or No Booking Found for this user");
        }
    }
}
