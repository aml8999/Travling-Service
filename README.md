# Traveling Services

### Author: Aml Osman

## Project Overview

Traveling Services is a C# Console Application that simulates a simple booking system for travel-related services. Users can register/login and then book Hotels, Flights, and Taxis. The project demonstrates OOP concepts (inheritance, enums, abstract classes), and uses in-memory collections (List, Dictionary) to manage data.

🔑 Features

User registration and login with role selection (Customer, HotelOwner, TaxiDriver, AirlineAdmin, SystemAdmin).

Hotel booking:

Multiple room types (Single, Double, Suite, Deluxe, Family, Presidential).

Room availability tracking and pricing by night.

Booking and cancellation for customers.

Flight booking:

Multiple destinations (enum-based country list).

Travel classes (Economy, Business, FirstClass) with automatic price and seat assignment.

Ticket printing with details (departure, arrival, duration).

Taxi booking:

Simple confirm/cancel flow for logged-in users.

Console-based interactive menu and robust input validation.

## 📁 Project Structure (high level)
/ (project root)
├─ Program.cs / Primary (contains the Main loop, routing & user session)
├─ Booking.cs (Booking base class)
├─ Person.cs / Customer.cs / HotelOwner etc.
├─ FlightBooking.cs / Flight.cs
├─ HotelBooking.cs / Hotel.cs
├─ TaxiBooking.cs / Taxi.cs
├─ ExceptionHandling.cs (input validation helpers)
├─ Enums.cs (TravelClass, Country, ServiceType)
└─ README.md



## 🛠️ Requirements

.NET SDK (SDK version compatible with your project, e.g., .NET 6 or later)

Install from: https://dotnet.microsoft.com/
 (if needed)

A terminal (Command Prompt / PowerShell / Git Bash) 

## 🧭 Usage (example flow)

Run the app.

Choose to Register or Login.

Registration asks for username, password, email, phone, and role.

After login, the Main Menu displays options:

1) Book a Hotel Room

2) Book a Flight

3) Book a Taxi

4) View Hotel Availability (HotelOwner)

5) Logout & Exit

Follow prompts to choose room types, destinations, travel class, number of nights, etc.

Booking confirmations are printed to the console; cancellations are supported for customer role.

🔎 Data Model & Key Classes (summary)

## Enums

 ServiceType — None, Customer, HotelOwner, TaxiDriver, AirlineAdmin, SystemAdmin

 TravelClass — None, Economy, Business, FirstClass

Country — (list of many countries used for flight origin/destination)

Person (abstract)

Base properties: Username, Password, Role, Email, PhoneNumber, LoggedIn

Customer : Person

Represents a customer user

Booking

Base booking class with BookingID, CustomerName, BookingDate

Hotel

Owns AvailableRooms dictionary and room management

HotelBooking : Booking

Room (enum), Nights, Price, confirm/cancel methods

FlightBooking : Flight : Booking

origin, destination, departureTime, arrivalTime, travelClass, SeatNumber, price, confirm/cancel/printTicket

TaxiBooking : Booking

simple confirm/cancel placeholders

ExceptionHandling

ValidInt and NonEmptyString helpers for validated console input

Session / Primary

Session holds current login state; Primary orchestrates register/login and main loop

## ✅ Input Validation & UX Notes

Input validation is centralized in ExceptionHandling. Use ValidInt for numeric prompts and NonEmptyString for required text.

Console colors are used to improve prompt readability (blue/yellow/red).

## 🧩 Extending the Project (ideas)

Persist users and bookings to a local file or simple database (JSON, SQLite).

Add editing/updating bookings and more robust booking IDs.

Add search/listing of bookings per user.

Add admin panel for SystemAdmin to manage users and bookings.

Add date selection for flights/hotels instead of fixed offsets.

Add more realistic seat selection and constraints.

## 🛡 License

Add a license of your choice (e.g., MIT). Example:
MIT License — include a LICENSE file if you want to open source it.

## 📬 Contact / Author

# Aml Osman — This project was implemented by Aml Osman.
If anyone needs help running or extending this project, contact Aml via the GitHub profile or project page.
