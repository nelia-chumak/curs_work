using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel_library
{
    //Delegate that will be used to change date and update information
    public delegate void TomorrowHandler();
    //Delegate that will be used to create events
    public delegate void RoomStateHandler(object sender, RoomEventArgs e);
    //Class for event handling
    public class RoomEventArgs
    {
        public string message { get; private set; } //message
        public int number { get; private set; }     //number of room

        public RoomEventArgs(string message1, int number1) //constructor with parameters
        {
            message = message1;
            number = number1;
        }
    }
}
