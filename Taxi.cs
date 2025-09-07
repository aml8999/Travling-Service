using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveling_Services
{
    class Taxi
    {


       
       


    }


    class TaxiBooking : Booking
    {
        public  void confirmBooking()
        {
            if (Primary.CurrentUser != null && Primary.CurrentUser.LoggedIn)
            {
                Console.WriteLine($"Taxi booking confirmed for {Primary.CurrentUser.Username}");
            }
            else
            {
                Console.WriteLine("⚠️ You must login first!");
            }
        }

        public  void cancelBooking()
        {
            if (Primary.CurrentUser != null && Primary.CurrentUser.LoggedIn)
            {
                Console.WriteLine("Taxi booking cancelled.");
            }
            else
            {
                Console.WriteLine("⚠️ You must login first!");
            }
        }
    }

}
