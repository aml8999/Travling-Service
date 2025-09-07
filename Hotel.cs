using System;
using System.Collections.Generic;
using System.Numerics;

namespace Traveling_Services
{
    class Hotel : Person
    {
        public Dictionary<HotelBooking.roomType, int> AvailableRooms { get; set; }

        public Hotel(string username, string password, ServiceType role, string email, BigInteger phone)
            : base(username, password, role, email, phone)
        {
            // مبدئياً نخلي فيه عدد معين من الغرف لكل نوع
            AvailableRooms = new Dictionary<HotelBooking.roomType, int>
            {
                { HotelBooking.roomType.Single, 5 },
                { HotelBooking.roomType.Double, 5 },
                { HotelBooking.roomType.Suite, 3 },
                { HotelBooking.roomType.Deluxe, 3 },
                { HotelBooking.roomType.Family, 2 },
                { HotelBooking.roomType.Presidential, 1 }
            };
        }

        // عرض الغرف للـ Owner
        public void ViewAvailableRooms()
        {
            Console.WriteLine("\n🏨 Hotel Room Availability:");
            foreach (var room in AvailableRooms)
            {
                Console.WriteLine($"{room.Key,-15} → {room.Value} rooms available");
            }
        }
    }

    class HotelBooking : Booking
    {
        public enum roomType
        {
            None = 0,
            Single,
            Double,
            Suite,
            Deluxe,
            Family,
            Presidential
        }

        public roomType Room { get; set; }
        public int Nights { get; set; }
        public decimal Price { get; set; }

        // جدول أسعار الغرف
        private static readonly Dictionary<roomType, decimal> RoomPrices = new Dictionary<roomType, decimal>
        {
            { roomType.Single, 500 },
            { roomType.Double, 800 },
            { roomType.Suite, 1500 },
            { roomType.Deluxe, 2000 },
            { roomType.Family, 2500 },
            { roomType.Presidential, 5000 }
        };

        public void confirmBooking(List<HotelBooking> hotelBookings, Hotel hotel)
        {
            if (Primary.CurrentUser != null && Primary.CurrentUser.Role == ServiceType.Customer)
            {
                // عرض كل الغرف المتاحة + أسعارها
                Console.WriteLine("\n Available Rooms:");
                foreach (var room in hotel.AvailableRooms)
                {
                    Console.WriteLine($"{(int)room.Key}. {room.Key,-12} | {RoomPrices[room.Key]} EGP/night | {room.Value} available");
                }

                // اختار نوع الغرفة
                var choice = ExceptionHandling.ValidInt("Enter Room Type (1.Single-6.Presidential): ", 1, 6);
                roomType selectedRoom = (roomType)choice;

                if (hotel.AvailableRooms[selectedRoom] > 0)
                {
                    int nights = ExceptionHandling.ValidInt("Enter nights: ", 1, 30);
                    decimal price = RoomPrices[selectedRoom] * nights;

                    // إنشاء حجز جديد
                    HotelBooking booking = new HotelBooking
                    {
                        CustomerName = Primary.CurrentUser.Username,
                        Room = selectedRoom,
                        Nights = nights,
                        Price = price,
                        BookingDate = DateTime.Now
                    };

                    // خصم من الغرف
                    hotel.AvailableRooms[selectedRoom]--;

                    // إضافة الحجز
                    hotelBookings.Add(booking);

                    Console.WriteLine($"\n Booking confirmed: {selectedRoom} for {nights} nights. Price: {price} EGP");
                }
                else
                {
                    Console.WriteLine($" Sorry, {selectedRoom} is fully booked.");
                }
            }
            else
            {
                Console.WriteLine(" Only customers can make hotel bookings.");
            }
        }

        public void cancelBooking(List<HotelBooking> hotelBookings, Hotel hotel)
        {
            if (Primary.CurrentUser != null && Primary.CurrentUser.Role == ServiceType.Customer)
            {
                // نبحث عن حجز لنفس المستخدم
                var booking = hotelBookings.Find(b => b.CustomerName == Primary.CurrentUser.Username);

                if (booking != null)
                {
                    // إرجاع الغرفة للـ AvailableRooms
                    hotel.AvailableRooms[booking.Room]++;

                    // حذف الحجز
                    hotelBookings.Remove(booking);

                    Console.WriteLine($"\n Booking for {booking.Room} cancelled. Room returned to availability.");
                }
                else
                {
                    Console.WriteLine(" No booking found for this customer.");
                }
            }
            else
            {
                Console.WriteLine(" Only customers can cancel hotel bookings.");
            }
        }
    }
}
