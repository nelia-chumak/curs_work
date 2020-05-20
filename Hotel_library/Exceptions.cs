using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Hotel_library
{
    //Exception for situations when room with this number is not found
    public class RoomNotFoundException: Exception
    {
        public RoomNotFoundException() { }
        public RoomNotFoundException(string message) : base(message) { }
    }
    //Exception trying to re-rent
    public class RoomIsRented : Exception
    {
        public RoomIsRented() { }
        public RoomIsRented(string message) : base(message) { }
    }
    //Exception trying to re-book
    public class RoomIsBooked : Exception
    {
        public RoomIsBooked() { }
        public RoomIsBooked(string message) : base(message) { }
    }
    //Exception for situations, when age of guest is immpossible
    public class ImpossibleAge : Exception
    {
        public ImpossibleAge() { }
        public ImpossibleAge(string message) : base(message) { }
    }
    //Exception for situations, when there are more guests than sleeping places
    public class TooManyGuests : Exception
    {
        public TooManyGuests() { }
        public TooManyGuests(string message) : base(message) { }
    }
    //Exception for situations, when date start booking/renting or date end booking/renting is wrong
    public class EndDateIsLessThenStartDate : Exception
    {
        public EndDateIsLessThenStartDate() { }
        public EndDateIsLessThenStartDate(string message) : base(message) { }
    }
}
