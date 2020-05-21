using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_library
{
    abstract public class Room: IRoom
    {
        protected internal event RoomStateHandler Rented; //event that occurs when renting a room
        protected internal event RoomStateHandler Booked; //event that occurs when booking a room

        public int number { get; protected set; }               //number of room
        public int area { get; protected set; }                 //area of room
        public int number_of_places { get; protected set; }     //number of places in room
        public bool Wifi { get; protected set; }                //presence of Wifi in room
        public RentedRoom data_of_renting;                      //all info about renting this room
        public BookedRoom[] data_of_booking = new BookedRoom[1];//all info about booking this room (array of notes)
        public class RentedRoom
        {
            public bool rented { get; internal set; }           //status of renting
            internal Guest[] guests;                            //array of guests
            internal DateTime date_from;                        //date start renting
            internal DateTime date_to;                          //date end renting
            //redifinition operator "+" for rent renewal
            public static RentedRoom operator +(RentedRoom thiss, int days)
            {
                thiss.date_to = thiss.date_to.AddDays(days);
                return thiss;
            }
        }
        public class BookedRoom
        {
            internal bool booked;                               //status of booking
            internal Guest[] guests;                            //array of guests
            internal DateTime date_from;                        //date start booking
            internal DateTime date_to;                          //date end booking
        }
        //method for renting a room
        public virtual void Rent(Guest[] guests1, int year_to, int month_to, int day_to)
        {
            DateTime date_to = new DateTime(year_to, month_to, day_to); //temporary variable for date end renting
            //check the number of guests accordind to the number of pleces
            if (guests1.Length > number_of_places)
                //if it is larger, throw exception type TooManyGuests
                throw new TooManyGuests("Too many guests for this number!");
            else if(guests1.Length < 1)
                //if it is less, throw exception type TooFewGuests
                throw new TooFewGuests("Too few guests for this number!");
            //check date end renting accordind to today date
            else if (date_to < DateTime.Today.AddDays(1))
                //if it is smaller throw exception type EndDateIsLessThenStartDate
                throw new EndDateIsLessThenStartDate("End date is less then start date or start");
            else {
                data_of_renting.rented = true;
                data_of_renting.guests = new Guest[guests1.Length];
                for (int i = 0; i < guests1.Length; i++)
                {
                    data_of_renting.guests[i] = guests1[i];
                }
                data_of_renting.date_from = DateTime.Today;
                data_of_renting.date_to = date_to;
            }
        }
        //method for booking a room
        public virtual void Book(Guest[] guests1, int year_from, int month_from, int day_from, int year_to, int month_to, int day_to)
        {
            int s = data_of_booking.Length-1; //variable for index of last note in booking array
            //temporary variable for date start booking
            DateTime date_from = new DateTime(year_from, month_from, day_from);
            //temporary variable for date end booking
            DateTime date_to = new DateTime(year_to, month_to, day_to);
            //check the number of guests accordind to the number of pleces
            if (guests1.Length > number_of_places)
                //if it is larger, throw exception type TooManyGuests
                throw new TooManyGuests("Too many guests for this number!");
            else if (guests1.Length < 1)
                //if it is less, throw exception type TooFewGuests
                throw new TooFewGuests("Too few guests for this number!");
            //check date end booking accordind to today date and start date
            if (date_from > date_to || date_from < DateTime.Today.AddDays(1))
                //if it is larger throw exception type EndDateIsLessThenStartDate
                throw new EndDateIsLessThenStartDate("End date is less then start date or start date is less then tomorrow");
            else
            {
                //increase array by one
                Array.Resize(ref data_of_booking, data_of_booking.Length + 1);
                //add new entry
                data_of_booking[s] = new BookedRoom();
                data_of_booking[s].booked = true;
                data_of_booking[s].guests = new Guest[guests1.Length];
                for (int i = 0; i < guests1.Length; i++)
                {
                    data_of_booking[s].guests[i] = guests1[i];
                }
                data_of_booking[s].date_from = date_from;
                data_of_booking[s].date_to = date_to;
            }
        }
        //method for pay room
        public abstract void Pay(ref decimal s_a, Guest[] guests, DateTime date_from, DateTime date_to);
        //method for return price of room
        public abstract decimal GetPrice(Guest[] guests, DateTime date_from, DateTime date_to);
        //method for calling events
        private void CallEvent(RoomEventArgs e, RoomStateHandler handler)
        {
            if (e != null && handler != null)
                handler(this, e);
        }
        //method for calling an event that occurs when booking a room
        protected void OnBooked(RoomEventArgs e)
        {
            CallEvent(e, Booked);
        }
        //method for calling an event that occurs when renting a room
        protected void OnRented(RoomEventArgs e)
        {
            CallEvent(e, Rented);
        }
    }
}
