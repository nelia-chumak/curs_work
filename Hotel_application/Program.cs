using System;
using Hotel_library;

namespace Hotel_application
{
    class Program
    {
        static void Main(string[] args)
        {
            Hotel Reikartz = new Hotel("Reikartz");                             //create new hotel
            Reikartz.rooms = new Room[3];                                       //craete roomms array
            Reikartz.rooms[0] = new StandardRoom(1, 20, 1, true);               //create first room
            Reikartz.rooms[1] = new JuniorSuiteRoom(2, 30, 2, true, false);     //second
            Reikartz.rooms[2] = new SuiteRoom(3, 40, 3, true, 2, "ordinary");   //third
            bool flag2 = true;      //set the flag to the truth to start the cycle
            int f = 0;              //variable to control indents
            while (flag2)
            {
                try
                {
                    //if it is first iteration (i=0), then no indents
                    if (f != 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else f++;
                    Console.WriteLine("Log in as:");
                    Console.WriteLine("1 Admin \n2 Guest  \n3 Exit ");
                    Console.WriteLine("Enter item number: ");
                    int type = Convert.ToInt32(Console.ReadLine()); //user's choice
                    if (type == 1)
                    {
                        bool flag = true;   //flag for admin cycle
                        bool flag1 = true;  //flag for password cycle
                        int password = 1;   //value of password
                        while (flag1 && password!=0)
                        {
                            Console.WriteLine("Enter password or '0' for log out: ");
                            password = Convert.ToInt32(Console.ReadLine()); //entered password
                            if (password == 1937) flag1 = false;            //if it is wrong new try
                            else if (password != 0)
                            {
                                PrintBuildInException("Password is wrong");
                            }
                            else flag = false;  //if person enter '0' return to main menu
                        }
                        while (flag)
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("What do you want to do?");
                            Console.WriteLine("1 Next day \n2 View settlement account");
                            Console.WriteLine("3 See info about all rooms  \n4 Log out");
                            Console.WriteLine("Enter item number: ");
                            try
                            {
                                int command = Convert.ToInt32(Console.ReadLine()); //user's choice

                                switch (command)
                                {
                                    case 1:
                                        if (Hotel.tomorrow == null) Hotel.tomorrow();  //set tomorrow's date and check status of rooms
                                        Console.WriteLine("Today is {0}", Hotel.current_date.ToString("d"));
                                        break;
                                    case 2:
                                        Get_settlement_account(Reikartz);  //display value of settlement account
                                        break;
                                    case 3:
                                        SeeInfo(Reikartz);                 //see info about all rooms
                                        break;
                                    case 4:
                                        flag = false;                      //exit
                                        continue;
                                    default:                               //if user don't input right number
                                        PrintBuildInException("This variant isn't right. Enter another.");
                                        break;
                                }
                            }
                            //catching exceptions
                            catch (System.FormatException)
                            {
                                PrintBuildInException("You entered data in the wrong format. Fill it all over again");
                            }
                            catch (System.OverflowException)
                            {
                                PrintBuildInException("You entered data is too big. Fill it all over again");
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                PrintBuildInException("You entered data in the wrong format. Fill it all over again");
                            }
                        }
                    }
                    else if (type == 2)
                    {
                        bool flag = true;   //flag for guest cycle
                        while (flag)
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("What do you want to do?");
                            Console.WriteLine("1 Book \n2 Rent \n3 Extend rent");
                            Console.WriteLine("4 See info about all rooms  \n5 Log out");
                            Console.WriteLine("Enter item number: ");
                            try
                            {
                                int command = Convert.ToInt32(Console.ReadLine()); //user's choice
                                switch (command)
                                {
                                    case 1:
                                        Book(Reikartz);     //booking a room
                                        break;
                                    case 2:
                                        Rent(Reikartz);     //renting a room
                                        break;
                                    case 3:
                                        Extend(Reikartz);   //rent renewal
                                        break;
                                    case 4:
                                        SeeInfo(Reikartz);  //see info about all rooms
                                        break;
                                    case 5:
                                        flag = false;       //exit
                                        continue;
                                    default:                //if user don't input right number
                                        PrintBuildInException("This variant isn't right. Enter another.");
                                        break;
                                }
                            }
                            //catching exceptions
                            catch (System.FormatException)
                            {
                                PrintBuildInException("You entered data in the wrong format. Fill it all over again");
                            }
                            catch (System.OverflowException)
                            {
                                PrintBuildInException("You entered data is too big. Fill it all over again");
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                PrintBuildInException("You entered data in the wrong format. Fill it all over again");
                            }
                            catch (RoomNotFoundException exception)
                            {
                                PrintException(exception);
                            }
                            catch (RoomIsRented exception)
                            {
                                PrintException(exception);
                            }
                            catch (RoomIsBooked exception)
                            {
                                PrintException(exception);
                            }
                            catch (ImpossibleAge exception)
                            {
                                PrintException(exception);
                            }
                            catch (TooManyGuests exception)
                            {
                                PrintException(exception);
                            }
                            catch (TooFewGuests exception)
                            {
                                PrintException(exception);
                            }
                            catch (EndDateIsLessThenStartDate exception)
                            {
                                PrintException(exception);
                            }
                        }
                    }
                    else if (type == 3) flag2 = false;
                    else
                    {
                        PrintBuildInException("This variant isn't right. Enter another.\n\n");
                    }
                }
                //catching exceptions
                catch (System.FormatException)
                {
                    PrintBuildInException("You entered data in the wrong format. Fill it all over again");
                }
                catch (System.OverflowException)
                {
                    PrintBuildInException("You entered data is too big. Fill it all over again");
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    PrintBuildInException("You entered data in the wrong format. Fill it all over again");
                }
            }
        }
        //methods for print info in red
        private static void PrintException(Exception exception)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exception.Message);
            Console.ForegroundColor = color;
        }
        private static void PrintBuildInException(string mess)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mess);
            Console.ForegroundColor = color;
        }
        //method for booking a room
        private static void Book(Hotel hotel)
        {
            int number, year_from, month_from, day_from, year_to, month_to, day_to;
            Guest[] guests;
            bool flag = true;
            (flag, number, guests, year_from, month_from, day_from, year_to, month_to, day_to) = Enter(hotel,1);
            if(flag) hotel.Book(BookHandler, number, guests, year_from, month_from, day_from, year_to, month_to, day_to);
        }
        //method for renting a room
        private static void Rent(Hotel hotel)
        {
            int number, year_from, month_from, day_from, year_to, month_to, day_to;
            Guest[] guests;
            bool flag = true;
            (flag, number, guests, year_from, month_from, day_from, year_to, month_to, day_to) = Enter(hotel,2);
            if (flag) hotel.Rent(RentHandler, number, guests, year_to, month_to, day_to);
        }
        //method for display value of settlement account
        private static void Get_settlement_account(Hotel hotel)
        {
            Console.WriteLine("In settlement account of {0} is {1} uah", hotel.name_of_hotel, hotel.get_settlement_account());
        }
        //print messages for events
        private static void RentHandler(object sender, RoomEventArgs e)
        {
            Console.WriteLine(e.message);
        }
        private static void BookHandler(object sender, RoomEventArgs e)
        {
            Console.WriteLine(e.message);
        }
        //method for enter data abot renting or booking
        private static (bool, int number, Guest[] guests, int year_from, int month_from, int day_from, int year_to, int month_to, int day_to) Enter(Hotel hotel, int type)
        {
            Console.WriteLine("Enter room number: ");
            int number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number of guests : ");
            int number_of_guests = Convert.ToInt32(Console.ReadLine());
            Guest[] guests = new Guest[number_of_guests];
            for (int i = 0; i < number_of_guests; i++)
            {
                Console.WriteLine("Enter name of " + (i + 1) + " guest: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter date of birth of " + (i + 1) + " guest in format dd.mm.yyyy: ");
                int year_of_birth, month_of_birth, day_of_birth;
                (year_of_birth, month_of_birth, day_of_birth) = SplitRead(Console.ReadLine());
                Console.WriteLine("Enter passport ID of " + (i + 1) + " guest (9 numbers): ");
                int passport_ID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter gender of " + (i + 1) + " guest ('feminin' or 'masculin'): ");
                string gender = Console.ReadLine();
                Guest guest = new Guest(name, year_of_birth, month_of_birth, day_of_birth, passport_ID, gender);
                guests[i] = guest;
            }
            int year_from = 0, month_from = 0, day_from = 0;
            int year_to = 0, month_to = 0, day_to = 0;
            if (type == 1)
            {
                Console.WriteLine("Enter booking start date in format dd.mm.yyyy : ");
                (year_from, month_from, day_from) = SplitRead(Console.ReadLine());
                Console.WriteLine("Enter booking end date in format dd.mm.yyyy : ");
                (year_to, month_to, day_to) = SplitRead(Console.ReadLine());
                DateTime from = new DateTime(year_from, month_from, day_from);
                DateTime to = new DateTime(year_to, month_to, day_to);
                Console.WriteLine("Price is {0}", hotel.get_price(number, guests, Hotel.current_date, to));
                Console.WriteLine("Do you want to book this number? Enter: \n1 for Yes \n0 for No ");
            }
            if (type == 2) 
            {
                Console.WriteLine("Enter renting end date in format dd.mm.yyyy : ");
                (year_to, month_to, day_to) = SplitRead(Console.ReadLine());
                DateTime to = new DateTime(year_to, month_to, day_to);
                Console.WriteLine("Price is {0}", hotel.get_price(number, guests, Hotel.current_date, to));
                Console.WriteLine("Do you want to rent this number? Enter: \n1 for Yes \n0 for No ");
            }
            int chosen = Convert.ToInt32(Console.ReadLine());
            if (chosen==1)  return (true, number, guests, year_from, month_from, day_from, year_to, month_to, day_to);
            else return (false, number, guests, year_from, month_from, day_from, year_to, month_to, day_to);
        }
        //metod to read date in format "dd.mm.yyyy"
        private static (int year, int day, int month) SplitRead(string date)
        {
            //variables for position tracking
            int count = 0;
            bool f = true;
            //variables for day, month and year value
            string year="", day="", month="";
            for(int i=0; i<date.Length; i++)
            {
                //to the first point, it's day
                if (date[i] != '.' && count == 0)
                {
                    day += date[i];
                }
                else if (date[i] == '.' && count == 0)
                {
                    count++;
                    f = false;
                }
                //to the second - month
                if (date[i] != '.' && count == 1)
                {
                    month += date[i];
                    f = true;
                }
                else if (date[i] == '.' && count == 1 && f==true) count++;
                //then year
                if (date[i] != '.' && count == 2)
                {
                    year += date[i];
                }
            }
            return (Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
        }
        //method for display info about all rooms
        private static void SeeInfo(Hotel hotel)
        {
            for(int i=0; i<hotel.rooms.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Info about room #{0}:", hotel.rooms[i].number);
                if(hotel.rooms[i].data_of_renting.rented == false) Console.WriteLine("It is available now");
                else Console.WriteLine("It isn't available now");
                Console.WriteLine("Area of room is {0} square meters", hotel.rooms[i].area);
                Console.WriteLine("Number of places is {0}",hotel.rooms[i].number_of_places);
                Console.WriteLine("Availability of Wifi is {0}", hotel.rooms[i].Wifi);
                if(hotel.rooms[i] is JuniorSuiteRoom)
                {
                    JuniorSuiteRoom link = (JuniorSuiteRoom)hotel.rooms[i];
                    if(link.support_3D_on_TV == true) Console.WriteLine("In this room is 3-d TV");
                    else Console.WriteLine("In this room isn't 3-d TV");
                }
                if (hotel.rooms[i] is SuiteRoom)
                {
                    SuiteRoom link = (SuiteRoom)hotel.rooms[i];
                    Console.WriteLine("Number of rooms is {0}", link.number_of_rooms);
                    Console.WriteLine("Type of balcony is {0}", link.type__of_balcony);
                }
            }
        }
        //method for rent renewal
        private static void Extend(Hotel hotel)
        {
            Console.WriteLine("Enter room number: ");
            int number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter amount of days: ");
            int amount = Convert.ToInt32(Console.ReadLine());
            //if such room exist
            if (number < hotel.rooms.Length)
            {
                //if room is rented now
                if (hotel.rooms[number - 1].data_of_renting.rented)
                {
                    hotel.rooms[number - 1].data_of_renting += amount;
                    Console.WriteLine("Operation was successfully completed");
                }
                else PrintBuildInException("This number is not currently rented!");
            }
            else PrintBuildInException("This number is not exist!");
        }
    }
}
