using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_library
{
    interface IRoom //Interface for roomms
    {
        //method for renting a room
        void Rent(Guest[] guests1, int year_to, int month_to, int day_to);
        //method for booking a room
        void Book(Guest[] guests1, int year_from, int month_from, int day_from, int year_to, int month_to, int day_to);
    }
}
