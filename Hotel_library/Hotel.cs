using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_library
{
    public class Hotel
    {
        public string name_of_hotel { get; private set; }        //name of hotel
        public Room[] rooms;                                     //array of hotel rooms
        private decimal settlement_account;                      //or checking account
        public static DateTime current_date = DateTime.Today;    //today's date
        public static TomorrowHandler tomorrow = null;                         //delegate variable
        //constructor with parameters
        public Hotel(string name_of_hotel1)
        {
            name_of_hotel = name_of_hotel1;
            tomorrow += change_current_date;
            tomorrow += Check;
        }
        //add one day
        public static void change_current_date()
        {
            current_date = current_date.AddDays(1);
        }
        //check status of rooms
        public void Check()
        {
            //if the period of rent is over, change the status to free
            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].data_of_renting.date_to == current_date)
                    rooms[i].data_of_renting.rented = false;
            }
            //if the start date of the reservation has arrived, change to the rented status
            for (int i = 0; i < rooms.Length; i++)
            {
                for (int j = 0; j < rooms[i].data_of_booking.Length - 1; j++)
                {
                    if (rooms[i].data_of_booking[j].date_from == current_date)
                    {
                        rooms[i].data_of_renting.rented = true;
                        rooms[i].data_of_renting.date_from = current_date;
                        rooms[i].data_of_renting.date_to = rooms[i].data_of_booking[j].date_to;
                        rooms[i].data_of_renting.guests = rooms[i].data_of_booking[j].guests;
                        for(int k=j; k< rooms[i].data_of_booking.Length - 1; k++)
                        {
                            rooms[i].data_of_booking[k] = rooms[i].data_of_booking[k + 1];
                        }
                        Array.Resize(ref rooms[i].data_of_booking, rooms[i].data_of_booking.Length - 1);
                    }
                }
            }
        }
        //return value of settlement account
        public decimal get_settlement_account()
        {
            return settlement_account;
        }
        //method for return price of room
        public decimal get_price(int number1, Guest[] guests, DateTime date_from, DateTime date_to)
        {
            Room room = FindRoom(number1);
            //if room exists
            if (room == null)
                throw new RoomNotFoundException("Room with such number not found!"); //trow exception RoomNotFoundException
            else
                return rooms[number1 - 1].GetPrice(guests, date_from, date_to);
        }
        //method for renting a room by number
        public void Rent(RoomStateHandler rentHandler, int number1, Guest[] guests1, int year_to, int month_to, int day_to)
        {
            Room room = FindRoom(number1);
            //if room exists
            if (room == null)
                throw new RoomNotFoundException("Room with such number not found!"); //trow exception RoomNotFoundException
            else
            {
                //and if it is free
                if (room.data_of_renting.rented == false)
                {
                    room.Rented += rentHandler; //subscribe method to event
                    //we rent it
                    room.Rent(guests1, year_to, month_to, day_to);
                    //and pay it
                    room.Pay(ref this.settlement_account, room.data_of_renting.guests, room.data_of_renting.date_from, room.data_of_renting.date_to);
                    room.Rented -= rentHandler;
                }
                else throw new RoomIsRented("Room is already rented!"); //trow exception type RoomIsRented
            }
        }
        //method for booking a room by number
        public void Book(RoomStateHandler bookHandler, int number1, Guest[] guests1, int year_from, int month_from, int day_from, int year_to, int month_to, int day_to)
        {
            Room room = FindRoom(number1);
            //if room exists
            if (room == null)
                throw new RoomNotFoundException("Room with such number not found!");//trow exception type RoomNotFoundException
            else
            {
                bool flag = true;
                DateTime from = new DateTime(year_from, month_from, day_from);
                DateTime to = new DateTime(year_to, month_to, day_to);
                //and if it is free
                for (int i = 0; i < room.data_of_booking.Length - 1; i++)
                {
                    if (to > room.data_of_booking[i].date_from && to < room.data_of_booking[i].date_to ||
                        from > room.data_of_booking[i].date_from && from < room.data_of_booking[i].date_to || from == current_date)
                        flag = false;
                }
                if (flag)
                {
                    room.Booked += bookHandler; //subscribe method to event
                    //we book it
                    room.Book(guests1, year_from, month_from, day_from, year_to, month_to, day_to);
                    int s = room.data_of_booking.Length - 2; //index of penultimate entry
                    //and pay it
                    room.Pay(ref this.settlement_account, room.data_of_booking[s].guests, room.data_of_booking[s].date_from, room.data_of_booking[s].date_to);
                    room.Booked -= bookHandler;
                }
                else throw new RoomIsBooked("Room is already booked!");//trow exception type RoomIsBooked
            }
        }
        //search for a room in array by number
        public Room FindRoom(int number1)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].number == number1)
                    return rooms[i];
            }
            return null;
        }
    }
}
