using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_library
{
    public class StandardRoom: Room
    {
        //constructor with parameters
        public StandardRoom(int number1, int area1, int number_of_places1, bool Wifi1)
        {
            number = number1;
            area = area1;
            number_of_places = number_of_places1;
            Wifi = Wifi1;
            RentedRoom data_of_renting1 = new RentedRoom();
            data_of_renting1.rented = false;
            data_of_renting = data_of_renting1;
            BookedRoom data_of_booking1 = new BookedRoom();
            data_of_booking1.booked = false;
            data_of_booking[0] = data_of_booking1;
        }
        //redefined method for renting a room
        public override void Rent(Guest[] guests1, int year_to, int month_to, int day_to)
        {
            base.Rent(guests1, year_to, month_to, day_to);
            //call event OnRented
            OnRented(new RoomEventArgs("You have rented a standard room with number " + this.number, this.number));
        }
        //redefined method for booking a room
        public override void Book(Guest[] guests1, int year_from, int month_from, int day_from, int year_to, int month_to, int day_to)
        {
            base.Book(guests1, year_from, month_from, day_from, year_to, month_to, day_to);
            //call event OnBooked
            OnBooked(new RoomEventArgs("You have booked a standard room with number " + this.number, this.number));
        }
        //redefined method for pay room
        public override void Pay(ref decimal s_a, Guest[] guests, DateTime date_from, DateTime date_to) 
        {
            s_a+=500*guests.Length*date_to.Subtract(date_from).Days;
        }
        //redefined method for return price of room
        public override decimal GetPrice(Guest[] guests, DateTime date_from, DateTime date_to)
        {
            return 500 * guests.Length * date_to.Subtract(date_from).Days;
        }
    }
}
