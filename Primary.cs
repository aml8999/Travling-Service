using System.Collections.Generic;
using System.Numerics;
using System;

namespace Traveling_Services
{
    public static class ExceptionHandling
    {
        public static int ValidInt(string prompt, int? min = null, int? max = null)
        {
            int value;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(prompt);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                var s = Console.ReadLine();
                Console.ResetColor();

                if (int.TryParse(s, out value))
                {
                    if ((min == null || value >= min) && (max == null || value <= max))
                        return value;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Please enter number between {min} and {max}.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input!!..Please enter a number.");
                    Console.ResetColor();
                }
            }
        }

        public static string NonEmptyString(string prompt)
        {
            string input;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(prompt);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                input = Console.ReadLine()?.Trim();
                Console.ResetColor();

                if (string.IsNullOrEmpty(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input cannot be empty.");
                    Console.ResetColor();
                }
            } while (string.IsNullOrEmpty(input));

            return input;
        }
    }

    public enum ServiceType
    {
        None = 0,
        Customer,
        HotelOwner,
        TaxiDriver,
        AirlineAdmin,
        SystemAdmin
    }

    public abstract class Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ServiceType Role { get; set; }
        public string Email { get; set; }
        public BigInteger PhoneNumber { get; set; }
        public bool LoggedIn { get; set; }

        protected Person() { }

        public Person(string username, string password, ServiceType role, string email, BigInteger phoneNumber)
        {
            Username = username;
            Password = password;
            Role = role;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }

    public class Booking
    {
        public string BookingID { get; set; }
        public string CustomerName { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking() : this("", "", DateTime.Now) { }

        public Booking(string bookingID, string customerName, DateTime bookingDate)
        {
            BookingID = bookingID;
            CustomerName = customerName;
            BookingDate = bookingDate;
        }
    }

    public class Customer : Person
    {
        public Customer(string username, string password, ServiceType role, string email, BigInteger phone)
            : base(username, password, role, email, phone) { }
    }
    public static class Primary
    {

        private static void RegisterOrLogin()
        {
            Console.WriteLine("Choose option: 1) Register  2) Login");
            string choice = Console.ReadLine();

            if (choice == "1")
                Register();
            else if (choice == "2")
                Login();
            else
                RegisterOrLogin();
        }
        
        private static void Register()
        {
            string username = ExceptionHandling.NonEmptyString("Enter username: ");
            string password = ExceptionHandling.NonEmptyString("Enter password: ");

            Console.WriteLine("\nAvailable Roles:");
            for (int i = 1; i < Enum.GetValues(typeof(ServiceType)).Length; i++)
            {
                ServiceType roleName = (ServiceType)Enum.GetValues(typeof(ServiceType)).GetValue(i);
                Console.WriteLine($"{(int)roleName}. {roleName}");
            }

            int roleChoice = ExceptionHandling.ValidInt("Enter role number: ", 1, Enum.GetValues(typeof(ServiceType)).Length - 1);
            ServiceType role = (ServiceType)roleChoice;

            string email = ExceptionHandling.NonEmptyString("Enter email: ");
            BigInteger phone = ExceptionHandling.ValidInt("Enter phone: ");

            Person user = new Customer(username, password, role, email, phone);
            users.Add(user);
            CurrentUser = user;
            user.LoggedIn = true;

            Console.WriteLine($" Registered & Logged in as {username} ({role})");
        }


        private static void Login()
        {
            string email = ExceptionHandling.NonEmptyString("Enter email: ");
            string password = ExceptionHandling.NonEmptyString("Enter password: ");

            foreach (var user in users)
            {
                if (user.Email == email && user.Password == password)
                {
                    CurrentUser = user;
                    user.LoggedIn = true;
                    Console.WriteLine("Login successful");
                    return;
                }
            }

            Console.WriteLine("Invalid email or password, try again.");
            
            RegisterOrLogin();
        }




        public static Person CurrentUser { get; private set; }
        private static List<Person> users = new List<Person>();

        public static void Main(string[] args)
        {
            Console.WriteLine(" Welcome to Traveling Services ");
            RegisterOrLogin();

            // قوائم للحجوزات
            List<HotelBooking> hotelBookings = new List<HotelBooking>();
            List<FlightBooking> flightBookings = new List<FlightBooking>();
            List<TaxiBooking> taxiBookings = new List<TaxiBooking>();

            // فندق افتراضي علشان فيه غرف
            Hotel hotel = new Hotel("GrandHotel", "1234", ServiceType.HotelOwner, "hotel@mail.com", 123456);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n===== Main Menu =====");
                Console.WriteLine("1) Book a Hotel Room");
                Console.WriteLine("2) Book a Flight");
                Console.WriteLine("3) Book a Taxi");
                Console.WriteLine("4) View Hotel Availability");
                Console.WriteLine("5) Logout & Exit");
                Console.ResetColor();
                
                int choice = ExceptionHandling.ValidInt("Choose option: ", 1, 5);

                if (choice == 1)
                {
                    HotelBooking hb = new HotelBooking();
                    hb.confirmBooking(hotelBookings, hotel);
                }
                else if (choice == 2)
                {
                    FlightBooking fb = new FlightBooking();
                    fb.confirmBooking(flightBookings);
                }
                else if (choice == 3)
                {
                    TaxiBooking tb = new TaxiBooking();
                    tb.confirmBooking();
                    taxiBookings.Add(tb);
                }
                else if (choice == 4)
                {
                    hotel.ViewAvailableRooms();
                }
                else if (choice == 5)
                {
                    Console.WriteLine("\n Thank you for using Traveling Services. Goodbye!");
                    break;
                }
            }
        }


    }


}
