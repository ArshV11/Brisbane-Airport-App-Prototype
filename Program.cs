using System;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

using System.Globalization;
using System.Security.AccessControl;
using Microsoft.Win32.SafeHandles;

namespace BrisbaneAirportApp
{
    /// <summary>
    /// The main entry point for the Brisbane Airport application.
    /// RESPONSIBILITY: Handles all console-based user interactions, including 
    /// displaying menus, managing registration and login workflows and delegating
    /// operational logic to the AirportSystem class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The primary instance of the AirportSystem used to manage users and flights.
        /// </summary>
        static AirportSystem sys = new();

        /// <summary>
        /// The main application header displayed to users on startup.
        /// </summary>
        static string menu1 = @"==========================================
=  Welcome to Brisbane Domestic Airport  =
==========================================";

        /// <summary>
        /// The initial menu displayed to users to prompt them to log in, register or exit.
        /// </summary>
        static string menu2 = @"
Please make a choice from the menu below:
1. Login as a registered user.
2. Register as a new user.
3. Exit.
Please enter a choice between 1 and 3:";

        /// <summary>
        /// The main entry point of this program. Displays the initial menus and processes user input.
        /// RESPONSIBILITY: Controls the top-level program flow and delegates to login or registration methods.
        /// </summary>
        public static void Main()
        {
            // Print the initial menu strings.
            Console.WriteLine(menu1);
            Console.WriteLine(menu2);

            // While loop dictating control flow of program.
            while (true)
            {
                var c = Read()?.Trim();
                if (string.IsNullOrEmpty(c))
                {
                    Console.WriteLine("Thank you. Safe travels.");
                    return;
                }
                if (c == "1")
                {
                    HandleLogin();
                }
                else if (c == "2")
                {
                    HandleRegister();
                }
                else if (c == "3")
                {
                    Console.WriteLine("Thank you. Safe travels.");
                    return;
                }
                else
                {
                    Console.WriteLine("Please enter a choice between 1 and 3:");
                    continue;
                }
                Console.WriteLine(menu2);
            }
        }

        /// <summary>
        /// Reads user input safely from the console.
        /// RESPONSIBILITY: Provides a fault-tolerant method for reading console inputs.
        /// </summary>
        /// <returns>The line of text entered by the user or NULL, if reading fails.</returns>
        static string Read()
        {
            try { return Console.ReadLine(); } catch { return null; }
        }

        /// <summary>
        /// Displays the registration menu and directs user to the appropriate registration process.
        /// RESPONSIBILITY: Acts as a dispatcher for the various user registration workflows.
        /// </summary>
        static void HandleRegister()
        {
            Console.WriteLine("Which user type would you like to register?\n1. A standard traveller.\n2. A frequent flyer.\n3. A flight manager.\nPlease enter a choice between 1 and 3:");
            var c = Read()?.Trim();
            if (c == "1")
            {
                RegTraveller();
            }
            else if (c == "2")
            {
                RegFrequentFlyer();
            }
            else if (c == "3")
            {
                RegFlightManager();
            }
            else
            {
                Console.WriteLine("Please enter a choice between 1 and 3:");
            }
        }

        /// <summary>
        /// Handles registration for a standard traveller.
        /// RESPONSIBILITY: Collects traveller information from user input, validates it, 
        /// and registers a new traveller via the AirportSystem sys.
        /// </summary>
        static void RegTraveller()
        {
            Console.WriteLine("Registering as a traveller.");

            Console.WriteLine("Please enter in your name:");

            // Use of helper method Read().
            var n = Read();

            // Use of Validators to validate user input for name and display corresponding error message.
            while (!Validators.ValidName(n))
            {
                Console.WriteLine("#####\n# Error - Supplied name is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your name:");
                n = Read();
            }

            Console.WriteLine("Please enter in your age between 0 and 99:");

            // Use of helper method Read().
            var a = Read();

            // Use of Validators to validate user input for age and display corresponding error message.
            while (!Validators.ValidAge(a))
            {
                if (a.All(c => char.IsLetter(c)))
                {
                    Console.WriteLine("#####\n# Error - Supplied value is invalid.\n# Please try again.\n#####");
                    Console.WriteLine("Please enter in your age between 0 and 99:");
                    a = Read();
                }
                else
                {
                    Console.WriteLine("#####\n# Error - Supplied age is invalid.\n# Please try again.\n#####");
                    Console.WriteLine("Please enter in your age between 0 and 99:");
                    a = Read();
                }
            }

            Console.WriteLine("Please enter in your mobile number:");

            // Use of helper method Read().
            var m = Read();

            // Use of Validators to validate user input for mobile number and display corresponding error message.
            while (!Validators.ValidMobile(m))
            {
                Console.WriteLine("#####\n# Error - Supplied mobile number is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your mobile number:");
                m = Read();
            }

            Console.WriteLine("Please enter in your email:");

            // Use of helper method Read().
            var e = Read();

            // Use of Validators and the EmailExists() helper method in AirportSystem sys to validate user input for email address and display corresponding error message.
            while (!Validators.ValidEmail(e))
            {
                Console.WriteLine("#####\n# Error - Supplied email is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your email:");
                e = Read();
            }
            while (sys.EmailExists(e))
            {
                Console.WriteLine("#####\n# Error - Email already registered.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your email:");
                e = Read();
            }

            Console.WriteLine("Please enter in your password:\nYour password must:\n-be at least 8 characters long \n-contain a number\n-contain a lowercase letter\n-contain an uppercase letter");

            // Use of helper method Read().
            var p = Read();

            // Use of Validators to validate user input for password and display corresponding error message.
            while (!Validators.ValidPassword(p))
            {
                Console.WriteLine("#####\n# Error - Supplied password is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your password:\nYour password must:\n-be at least 8 characters long \n-contain a number\n-contain a lowercase letter\n-contain an uppercase letter");
                p = Read();
            }

            // Once all inputs have been validated, use the helper method RegisterTraveller() in the AirportSystem instance sys.
            Console.WriteLine(sys.RegisterTraveller(n, Convert.ToInt32(a), m, e, p));
        }

        /// <summary>
        /// Handles registration for a frequent flyer.
        /// RESPONSIBILITY: Collects and validates user information for frequent flyer registration and passes it to AirportSystem.
        /// </summary>
        static void RegFrequentFlyer()
        {
            Console.WriteLine("Registering as a frequent flyer.");

            Console.WriteLine("Please enter in your name:");

            // Use of helper method Read().
            var n = Read();

            // Use of Validators to validate user input for name and display corresponding error message.
            while (!Validators.ValidName(n))
            {
                Console.WriteLine("#####\n# Error - Supplied name is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your name:");
                n = Read();
            }

            Console.WriteLine("Please enter in your age between 0 and 99:");

            // Use of helper method Read().
            var a = Read();

            // Use of Validators to validate user input for age and display corresponding error message.
            while (!Validators.ValidAge(a))
            {
                if (a.All(c => char.IsLetter(c)))
                {
                    Console.WriteLine("#####\n# Error - Supplied value is invalid.\n# Please try again.\n#####");
                    Console.WriteLine("Please enter in your age between 0 and 99:");
                    a = Read();
                }
                else
                {
                    Console.WriteLine("#####\n# Error - Supplied age is invalid.\n# Please try again.\n#####");
                    Console.WriteLine("Please enter in your age between 0 and 99:");
                    a = Read();
                }
            }

            Console.WriteLine("Please enter in your mobile number:");

            // Use of helper method Read().
            var m = Read();

            // Use of Validators to validate user input for mobile number and display corresponding error message.
            while (!Validators.ValidMobile(m))
            {
                Console.WriteLine("#####\n# Error - Supplied mobile number is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your mobile number:");
                m = Read();
            }

            Console.WriteLine("Please enter in your email:");

            // Use of helper method Read().
            var e = Read();

            // Use of Validators and the EmailExists() helper method in AirportSystem sys to validate user input for email address and display corresponding error message.
            while (!Validators.ValidEmail(e))
            {
                Console.WriteLine("#####\n# Error - Supplied email is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your email:");
                e = Read();
            }
            while (sys.EmailExists(e))
            {
                Console.WriteLine("#####\n# Error - Email already registered.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your email:");
                e = Read();
            }

            Console.WriteLine("Please enter in your password:\nYour password must:\n-be at least 8 characters long \n-contain a number\n-contain a lowercase letter\n-contain an uppercase letter");

            // Use of helper method Read().
            var p = Read();

            // Use of Validators to validate user input for password and display corresponding error message.
            while (!Validators.ValidPassword(p))
            {
                Console.WriteLine("#####\n# Error - Supplied password is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your password:\nYour password must:\n-be at least 8 characters long \n-contain a number\n-contain a lowercase letter\n-contain an uppercase letter");
                p = Read();
            }

            Console.WriteLine("Please enter in your frequent flyer number between 100000 and 999999:");

            // Use of helper method Read().
            var fn = Read();

            // Use of Validators to validate user input for the frequent flyer number and display corresponding error message.
            while (!Validators.ValidFFNumber(Convert.ToInt32(fn)))
            {
                Console.WriteLine("#####\n# Error - Supplied frequent flyer number is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your frequent flyer number between 100000 and 999999:");
                fn = Read();
            }

            Console.WriteLine("Please enter in your current frequent flyer points between 0 and 1000000:");

            // Use of helper method Read().
            var fp = Read();

            // Use of Validators to validate user input for the frequent flyer points and display corresponding error message.
            while (!Validators.ValidFFPoints(Convert.ToInt32(fp)))
            {
                Console.WriteLine("#####\n# Error - Supplied current frequent flyer points is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your current frequent flyer points between 0 and 1000000:");
                fp = Read();
            }

            // Once all inputs have been validated, use the helper method RegisterFrequentFlyer() in the AiportSystem instance sys.
            Console.WriteLine(sys.RegisterFrequentFlyer(n, Convert.ToInt32(a), m, e, p, Convert.ToInt32(fn), Convert.ToInt32(fp)));
        }

        /// <summary>
        /// Handles registration for a flight manager.
        /// RESPONSIBILITY: collects and validates user infromation for flight managers and passes it to AirportSystem.
        /// </summary>
        static void RegFlightManager()
        {
            Console.WriteLine("Registering as a flight manager.");

            Console.WriteLine("Please enter in your name:");

            // Use of helper method Read().
            var n = Read();

            // Use of Validators to validate user input for name and display corresponding error message.
            while (!Validators.ValidName(n))
            {
                Console.WriteLine("#####\n# Error - Supplied name is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your name:");
                n = Read();
            }

            Console.WriteLine("Please enter in your age between 0 and 99:");

            // Use of helper method Read().
            var a = Read();

            // Use of Validators to validate user input for age and display corresponding error message.
            while (!Validators.ValidAge(a))
            {
                if (a.All(c => char.IsLetter(c)))
                {
                    Console.WriteLine("#####\n# Error - Supplied value is invalid.\n# Please try again.\n#####");
                    Console.WriteLine("Please enter in your age between 0 and 99:");
                    a = Read();
                }
                else
                {
                    Console.WriteLine("#####\n# Error - Supplied age is invalid.\n# Please try again.\n#####");
                    Console.WriteLine("Please enter in your age between 0 and 99:");
                    a = Read();
                }
            }

            Console.WriteLine("Please enter in your mobile number:");

            // Use of helper method Read().
            var m = Read();

            // Use of Validators to validate user input for mobile number and display corresponding error message.
            while (!Validators.ValidMobile(m))
            {
                Console.WriteLine("#####\n# Error - Supplied mobile number is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your mobile number:");
                m = Read();
            }

            Console.WriteLine("Please enter in your email:");

            // Use of helper method Read().
            var e = Read();

            // Use of Validators and the EmailExists() helper method in AirportSystem sys to validate user input for email address and display corresponding error message.
            while (!Validators.ValidEmail(e))
            {
                Console.WriteLine("#####\n# Error - Supplied email is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your email:");
                e = Read();
            }
            while (sys.EmailExists(e))
            {
                Console.WriteLine("#####\n# Error - Email already registered.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your email:");
                e = Read();
            }

            Console.WriteLine("Please enter in your password:\nYour password must:\n-be at least 8 characters long \n-contain a number\n-contain a lowercase letter\n-contain an uppercase letter");

            // Use of helper method Read().
            var p = Read();

            // Use of Validators to validate user input for password and display corresponding error message.
            while (!Validators.ValidPassword(p))
            {
                Console.WriteLine("#####\n# Error - Supplied password is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your password:\nYour password must:\n-be at least 8 characters long \n-contain a number\n-contain a lowercase letter\n-contain an uppercase letter");
                p = Read();
            }

            Console.WriteLine("Please enter in your staff id between 1000 and 9000:");

            // Use of helper method Read().
            var s = Read();

            // Use of Validators to validate user input for staff ID and display corresponding error message.
            while (!Validators.ValidStaffId(Convert.ToInt32(s)))
            {
                Console.WriteLine("#####\n# Error - Supplied staff id is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your staff id between 1000 and 9000:");
                s = Read();
            }

            // Once all inputs have been validated, use the helper method RegisterFlightManager() in the AirportSystem instance sys.
            Console.WriteLine(sys.RegisterFlightManager(n, Convert.ToInt32(a), m, e, p, s));
        }

        /// <summary>
        /// Handles the user login process
        /// RESPONSIBILITY: Authenticates registered users and initiates user-specific operations within AirportSystem.
        /// </summary>
        static void HandleLogin()
        {
            Console.WriteLine("Login Menu.");

            // If no users have been created, return the corresponding error message and return to the main menu.
            if (!sys.HasUsers())
            {
                Console.WriteLine("#####\n# Error - There are no people registered.\n#####");
                return;
            }


            Console.WriteLine("Please enter in your email:");

            // Use of helper method Read().
            var e = Read();

            // Use of Validators and helper function EmailExists() in AirportSystem sys to validate user input for name and display corresponding error message.
            while (!Validators.ValidEmail(e))
            {
                Console.WriteLine("#####\n# Error - Supplied email is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your email:");
                e = Read();
            }
            while (!sys.EmailExists(e))
            {
                Console.WriteLine("#####\n# Error - Email is not registered.\n#####");
                Console.WriteLine("Please enter in your email:");
                e = Read();
            }

            Console.WriteLine("Please enter in your password:");

            // Use of helper method Read().
            var p = Read();

            // Use of Validators to validate user input for password and display corresponding error message.
            while (!Validators.ValidPassword(p))
            {
                Console.WriteLine("#####\n# Error - Supplied password is invalid.\n# Please try again.\n#####");
                Console.WriteLine("Please enter in your password:");
                p = Read();
            }

            // Use of helper function Login() in AirportSystem instance sys to authenticate the password.
            var u = sys.Login(e?.Trim() ?? "", p ?? "");
            while (u == null)
            {
                Console.WriteLine("#####\n# Error - Incorrect Password.\n#####");
                Console.WriteLine("Please enter in your password:");
                p = Read();
                u = sys.Login(e?.Trim() ?? "", p ?? "");
            }

            // Once user has been validated and authenticated, prompt the relevant menu options for the type of user.
            Console.WriteLine($"Welcome back {u.Name}.\n");
            if (u is FlightManager fm)
            {
                FMMenu(fm);
            }
            else if (u is FrequentFlyer ff)
            {
                FFMenu(ff);
            }
            else if (u is Traveller t)
            {
                TMenu(t);
            }
        }

        /// <summary>
        /// Displays and manages the flight manager menu interface.
        /// RESPONSIBILITY: Provides access to administrative functionalities such as flight scheduling, seat assignment 
        /// and management of bookings and passenger information.
        /// </summary>
        /// <param name="fm">The FlightManager instance fm that has logged in.</param>
        static void FMMenu(FlightManager fm)
        {
            // While loop dictating control flow whilst logged in as a flight manager.
            while (true)
            {
                Console.WriteLine("Flight Manager Menu.\nPlease make a choice from the menu below:");
                Console.WriteLine("1. See my details.\n2. Change password.\n3. Create an arrival flight.\n4. Create a departure flight.\n5. Delay an arrival flight.\n6. Delay a departure flight.\n7. See the details of all flights.\n8. Logout.");
                Console.WriteLine("Please enter a choice between 1 and 8:");
                var c = Read()?.Trim();

                // See flight manager details.
                if (c == "1")
                {
                    Console.WriteLine("Your details.");

                    // Use of helper method DisplayInfo() in FlightManager instance fm.
                    Console.WriteLine(fm.DisplayInfo());
                }

                // Changing password for flight manager.
                else if (c == "2")
                {
                    Console.WriteLine("Please enter your current password.");

                    // Use of helper method Read().
                    var old = Read();

                    // Use of helper function Authenticate() to validate user input for old password and display corresponding error message.
                    while (!fm.Authenticate(old))
                    {
                        Console.WriteLine("#####\n# Error - Entered password does not match existing password.\n# Please try again.\n#####");
                        Console.WriteLine("Please enter your current password."); old = Read();
                    }
                    Console.WriteLine("Please enter your new password.");

                    // Use of helper method Read().
                    var newp = Read();

                    // Use of ChangePassword() helper function to change password of flight manager.
                    fm.ChangePassword(old, newp);
                }

                // Creating an arrival flight.
                else if (c == "3")
                {
                    // Use of helper method CreateArr().
                    CreateArr();
                }

                // Creating a departure flight.
                else if (c == "4")
                {
                    // Use of helper method CreateDep().
                    CreateDep();
                }

                // Delaying an arrival flight.
                else if (c == "5")
                {
                    // Use of helper method DelayArrivalFlight().
                    DelayArrivalFlight();
                }

                // Delaying a departure flight.
                else if (c == "6")
                {
                    // Use of helper method DelayDepartureFlight().
                    DelayDepartureFlight();
                }

                // Displaying all current flights in chronological order
                else if (c == "7")
                {
                    // Use of helper method ShowAllFlightsChronological() from AirportSystem instance sys.
                    sys.ShowAllFlightsChronological();
                }

                // Logging out of the flight manager menu.
                else if (c == "8")
                {
                    return;
                }

                else Console.WriteLine("Invalid choice");
            }
        }

        /// <summary>
        /// Displays and manages the Traveller user menu interface.
        /// RESPONSIBILITY: Provides travellers with access to essential flight information including 
        /// viewing, booking or cancelling flight reservations.
        /// </summary>
        /// <param name="t">The Traveller instance t that has logged in.</param>
        static void TMenu(Traveller t)
        {
            // While loop dictating control flow whilst logged in as a traveller.
            while (true)
            {
                Console.WriteLine("Traveller Menu.\nPlease make a choice from the menu below:");
                Console.WriteLine("1. See my details.\n2. Change password.\n3. Book an arrival flight.\n4. Book a departure flight.\n5. See flight details.\n6. Logout.");
                Console.WriteLine("Please enter a choice between 1 and 6:");
                var c = Read()?.Trim();

                // See traveller details.
                if (c == "1")
                {
                    Console.WriteLine("Your details.");

                    // Use of helper method DisplayInfo() in Traveller instance t
                    Console.WriteLine(t.DisplayInfo());
                }

                // Changing password for a traveller.
                else if (c == "2")
                {
                    Console.WriteLine("Please enter your current password.");

                    // Use of helper method Read().
                    var old = Read();

                    // Use of helper function Authenticate() to validate user input for password and display corresponding error message.
                    while (!t.Authenticate(old))
                    {
                        Console.WriteLine("#####\n# Error - Entered password does not match existing password.\n# Please try again.\n#####");
                        Console.WriteLine("Please enter your current password.");
                        old = Read();
                    }
                    Console.WriteLine("Please enter your new password.");

                    // Use of helper method Read().
                    var newp = Read();

                    // Use of ChangePassword() helper function to change password of traveller.
                    t.ChangePassword(old, newp);
                }

                // Book an arrival flight as a traveller.
                else if (c == "3")
                {
                    // Traveller can only have one arrival flight booked at a time.
                    if (t.ArrivalBooking != null)
                    {
                        Console.WriteLine("#####\n# Error - You already have an arrival flight. You can not book another.\n#####");
                    }
                    else
                    {
                        bool validFlightSelected = false;
                        ArrivalFlight selectedArrivalFlight;
                        var f = "";

                        // While loop handling user prompting and returning corresponding error message while a valid flight has not yet been selected.
                        while (!validFlightSelected)
                        {
                            Console.WriteLine("Please enter the arrival flight:");

                            // Use of ShowArrivalFlightsChronological() helper function from AirportSystem sys.
                            var (msg, flightCount, flights) = sys.ShowArrivalFlightsChronological();
                            Console.WriteLine(msg);
                            Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");

                            // Use of helper function Read().
                            f = Read();

                            // Ensures user selects valid available flight.
                            while (!int.TryParse(f, out int flightNum) || flightNum < 1 || flightNum > flightCount)
                            {
                                Console.WriteLine("#####\n# Error - Supplied value is out of range.\n# Please try again.\n#####");
                                Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");
                                f = Read();
                            }

                            selectedArrivalFlight = flights[int.Parse(f) - 1];

                            // If the traveller has a pre-existing departure booking, this ensures that the arrival flight is scheduled to arrive 
                            // before the departure of the departure flight. 
                            if (t.DepartureBooking != null)
                            {
                                // Use of GetFlightByPlaneId() helper function in AirportSystem instance sys.
                                var departureFlight = sys.GetFlightByPlaneId(t.DepartureBooking.FlightPlaneId);
                                if (selectedArrivalFlight.Scheduled >= departureFlight.Scheduled)
                                {
                                    Console.WriteLine("#####\n# Error - The arrival time must be before the departure time.\n# Please try again.\n#####");
                                    continue;
                                }
                            }

                            // Exit while loop once valid flight has been selected.
                            validFlightSelected = true;
                        }

                        // Once valid flight is selected, proceed with seat bookings.
                        Console.WriteLine("Please enter in your seat row between 1 and 10:");

                        // Use of helper function Read().
                        var seat_r = Read();

                        // While loop validating that the selected seat row is greater than one and less than ten.
                        while (Convert.ToInt32(seat_r) < 1 || Convert.ToInt32(seat_r) > 10)
                        {
                            Console.WriteLine("#####\n# Error - Supplied seat row is invalid.\n# Please try again.\n#####");
                            Console.WriteLine("Please enter in your seat row between 1 and 10:");
                            seat_r = Read();
                        }

                        Console.WriteLine("Please enter in your seat column between A and D:");

                        // Use of helper function Read().
                        var seat_c = Read();

                        // Use of Validators to validate user input for the selected seat colunm
                        while (!Validators.ValidSeatColumn(seat_c))
                        {
                            Console.WriteLine("#####\n# Error - Supplied seat column is invalid.\n# Please try again.\n#####");
                            Console.WriteLine("Please enter in your seat column between A and D:");
                            seat_c = Read();
                        }

                        string seat = seat_r + seat_c;

                        // Use of helper function BookFlight() from AirportSystem instance sys to book seat on flight for traveller.
                        var result = sys.BookFlight(t.Email, int.Parse(f), seat, true);
                        Console.WriteLine(result.msg);
                    }
                }

                // Book a departure flight as a traveller.
                else if (c == "4")
                {
                    // Traveller can only have one departure flight booked at a time.
                    if (t.DepartureBooking != null)
                    {
                        Console.WriteLine("#####\n# Error - You already have a departure flight. You can not book another.\n#####");
                    }
                    else
                    {
                        bool validFlightSelected = false;
                        DepartureFlight selectedDepartureFlight;
                        var f = "";

                        // While loop handling user prompting and returning corresponding error message while a valid flight has not yet been selected.
                        while (!validFlightSelected)
                        {
                            Console.WriteLine("Please enter the departure flight:");

                            // Use of ShowArrivalFlightsChronological() helper function from AirportSystem sys.
                            var (msg, flightCount, flights) = sys.ShowDepartureFlightsChronological();
                            Console.WriteLine(msg);
                            Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");

                            // Use of helper function Read().
                            f = Read();

                            // Ensures user selects valid available flight.
                            while (!int.TryParse(f, out int flightNum) || flightNum < 1 || flightNum > flightCount)
                            {
                                Console.WriteLine("#####\n# Error - Supplied value is out of range.\n# Please try again.\n#####");
                                Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");
                                f = Read();
                            }

                            selectedDepartureFlight = flights[int.Parse(f) - 1];

                            // If the traveller has a pre-existing arrival booking, this ensures that the departure flight is scheduled to depart 
                            // after the arrival of the arrival flight. 
                            if (t.ArrivalBooking != null)
                            {
                                // Use of GetFlightByPlaneId() helper function in AirportSystem instance sys.
                                var arrivalFlight = sys.GetFlightByPlaneId(t.ArrivalBooking.FlightPlaneId);
                                if (selectedDepartureFlight.Scheduled <= arrivalFlight.Scheduled)
                                {
                                    Console.WriteLine("#####\n# Error - The departure time must be after the arrival time.\n# Please try again.\n#####");
                                    continue;
                                }
                            }

                            // Exit while loop once valid flight has been selected.
                            validFlightSelected = true;
                        }

                        // Once valid flight is selected, proceed with seat bookings.
                        Console.WriteLine("Please enter in your seat row between 1 and 10:");

                        // Use of helper function Read().
                        var seat_r = Read();

                        // While loop validating that the selected seat row is greater than one and less than ten.
                        while (Convert.ToInt32(seat_r) < 1 || Convert.ToInt32(seat_r) > 10)
                        {
                            Console.WriteLine("#####\n# Error - Supplied seat row is invalid.\n# Please try again.\n#####");
                            Console.WriteLine("Please enter in your seat row between 1 and 10:");
                            seat_r = Read();
                        }

                        Console.WriteLine("Please enter in your seat column between A and D:");

                        // Use of helper function Read().
                        var seat_c = Read();

                        // Use of Validators to validate user input for the selected seat colunm
                        while (!Validators.ValidSeatColumn(seat_c))
                        {
                            Console.WriteLine("#####\n# Error - Supplied seat column is invalid.\n# Please try again.\n#####");
                            Console.WriteLine("Please enter in your seat column between A and D:");
                            seat_c = Read();
                        }

                        string seat = seat_r + seat_c;

                        // Use of helper function BookFlight() from AirportSystem instance sys to book seat on flight for traveller.
                        var result = sys.BookFlight(t.Email, int.Parse(f), seat, false);
                        Console.WriteLine(result.msg);
                    }
                }

                // See all booked flight details as a traveller.
                else if (c == "5")
                {
                    Console.WriteLine($"Showing flight details for {t.Name}:");

                    // If an arrival flight booking exists for the traveller, return the flight details as a formatted string.
                    if (t.ArrivalBooking != null)
                    {
                        // Use of GetFlightByPlaneId() helper function in the AirportSystem instance sys.
                        var arrivalFlight = sys.GetFlightByPlaneId(t.ArrivalBooking.FlightPlaneId);
                        if (arrivalFlight is ArrivalFlight af)
                        {
                            string formattedSeat = $"{t.ArrivalBooking.Seat[..^1]}:{t.ArrivalBooking.Seat[^1]}";
                            Console.WriteLine($"Arrival Flight: Flight {arrivalFlight.Airline}{arrivalFlight.FlightCode} from {af.DepartureCity} arriving at {arrivalFlight.Scheduled:HH:mm dd/MM/yyyy} in seat {formattedSeat}.");
                        }
                    }

                    // If a departure flight booking exists for the traveller, return the flight details as a formatted string.
                    if (t.DepartureBooking != null)
                    {
                        // Use of GetFlightByPlaneId() helper function in the AirportSystem instance sys.
                        var departureFlight = sys.GetFlightByPlaneId(t.DepartureBooking.FlightPlaneId);
                        if (departureFlight is DepartureFlight df)
                        {
                            string formattedSeat = $"{t.DepartureBooking.Seat[..^1]}:{t.DepartureBooking.Seat[^1]}";
                            Console.WriteLine($"Departure Flight: Flight {departureFlight.Airline}{departureFlight.FlightCode} to {df.ArrivalCity} departing at {departureFlight.Scheduled:HH:mm dd/MM/yyyy} in seat {formattedSeat}.");
                        }
                    }
                }

                // Log out from the traveller menu.
                else if (c == "6")
                {
                    return;
                }

                else Console.WriteLine("Invalid choice");
            }
        }

        /// <summary>
        /// Displays and manages the frequent flyer user menu interface.
        /// RESPONSIBILITY: Provides frequent flyers with the ability to view their profile information,
        /// manage their bookings and track or redeem their frequent flyer points.
        /// </summary>
        /// <param name="ff">The FrequentFlyer instance that has logged in.</param>
        static void FFMenu(FrequentFlyer ff)
        {
            // While loop dictating control flow whilst logged in as a traveller.
            while (true)
            {
                Console.WriteLine("Frequent Flyer Menu.\nPlease make a choice from the menu below:");
                Console.WriteLine("1. See my details.\n2. Change password.\n3. Book an arrival flight.\n4. Book a departure flight.\n5. See flight details.\n6. See frequent flyer points.\n7. Logout.");
                Console.WriteLine("Please enter a choice between 1 and 7:");
                var c = Read()?.Trim();

                // See frequent flyer details
                if (c == "1")
                {
                    Console.WriteLine("Your details.");

                    // Use of helper method DisplayInfo() in FrequentFlyer instance t
                    Console.WriteLine(ff.DisplayInfo());
                }

                // Change password for a frequent flyer.
                else if (c == "2")
                {
                    Console.WriteLine("Please enter your current password.");

                    // Use of helper function Read().
                    var old = Read();

                    // Use of helper function Authenticate() to validate user input for password and display corresponding error message.
                    while (!ff.Authenticate(old))
                    {
                        Console.WriteLine("#####\n# Error - Entered password does not match existing password.\n# Please try again.\n#####");
                        Console.WriteLine("Please enter your current password.");
                        old = Read();
                    }
                    Console.WriteLine("Please enter your new password.");

                    // Use of helper function Read().
                    var newp = Read();

                    // Use of ChangePassword() helper function to change password of frequent flyer.
                    ff.ChangePassword(old, newp);
                }

                // Book an arrival flight as a frequent flyer.
                else if (c == "3")
                {
                    // Frequent flyer can only have one arrival flight booked at a time.
                    if (ff.ArrivalBooking != null)
                    {
                        Console.WriteLine("#####\n# Error - You already have an arrival flight. You can not book another.\n#####");
                    }
                    else
                    {
                        bool validFlightSelected = false;
                        ArrivalFlight selectedArrivalFlight;
                        var f = "";

                        // While loop handling user prompting and returning corresponding error message while a valid flight has not yet been selected.
                        while (!validFlightSelected)
                        {
                            Console.WriteLine("Please enter the arrival flight:");

                            // Use of ShowArrivalFlightsChronological() helper function from AirportSystem sys.
                            var (msg, flightCount, flights) = sys.ShowArrivalFlightsChronological();
                            Console.WriteLine(msg);
                            Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");

                            // Use of helper function Read().
                            f = Read();

                            // Ensures user selects valid available flight.
                            while (!int.TryParse(f, out int flightNum) || flightNum < 1 || flightNum > flightCount)
                            {
                                Console.WriteLine("#####\n# Error - Supplied value is out of range.\n# Please try again.\n#####");
                                Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");
                                f = Read();
                            }

                            selectedArrivalFlight = flights[int.Parse(f) - 1];


                            // If the traveller has a pre-existing departure booking, this ensures that the arrival flight is scheduled to arrive 
                            // before the departure of the departure flight. 
                            if (ff.DepartureBooking != null)
                            {
                                // Use of GetFlightByPlaneId() helper function in AirportSystem instance sys.
                                var departureFlight = sys.GetFlightByPlaneId(ff.DepartureBooking.FlightPlaneId);
                                if (selectedArrivalFlight.Scheduled >= departureFlight.Scheduled)
                                {
                                    Console.WriteLine("#####\n# Error - The arrival time must be before the departure time.\n# Please try again.\n#####");
                                    continue;
                                }
                            }

                            // Exit while loop once valid flight has been selected.
                            validFlightSelected = true;
                        }

                        // Once valid flight is booked, proceed with seat bookings.
                        Console.WriteLine("Please enter in your seat row between 1 and 10:");

                        // Use of helper function Read().
                        var seat_r = Read();

                        // While loop validating that the selected seat row is greater than one and less than ten.
                        while (Convert.ToInt32(seat_r) < 1 || Convert.ToInt32(seat_r) > 10)
                        {
                            Console.WriteLine("#####\n# Error - Supplied seat row is invalid.\n# Please try again.\n#####");
                            Console.WriteLine("Please enter in your seat row between 1 and 10:");
                            seat_r = Read();
                        }

                        Console.WriteLine("Please enter in your seat column between A and D:");

                        // Use of helper function Read().
                        var seat_c = Read();

                        // Use of Validators to validate user input for the selected seat column.
                        while (!Validators.ValidSeatColumn(seat_c))
                        {
                            Console.WriteLine("#####\n# Error - Supplied seat column is invalid.\n# Please try again.\n#####");
                            Console.WriteLine("Please enter in your seat column between A and D:");
                            seat_c = Read();
                        }

                        string seat = seat_r + seat_c;

                        // Use of helper function BookFlight() from AirportSystem instance sys to book seat on flight for frequent flyer.
                        var result = sys.BookFlight(ff.Email, int.Parse(f), seat, true);
                        Console.WriteLine(result.msg);
                    }
                }

                // Book a departure flight as a frequent flyer.
                else if (c == "4")
                {
                    // Frequent flyer can only have one departure flight booked at a time.
                    if (ff.DepartureBooking != null)
                    {
                        Console.WriteLine("#####\n# Error - You already have a departure flight. You can not book another.\n#####");
                    }
                    else
                    {
                        bool validFlightSelected = false;
                        DepartureFlight selectedDepartureFlight;
                        var f = "";

                        // While loop handling user prompting and returning corresponding error message while a valid flight has not yet been selected.
                        while (!validFlightSelected)
                        {
                            Console.WriteLine("Please enter the departure flight:");

                            // Use of ShowArrivalFlightsChronological() helper function from AirportSystem sys.
                            var (msg, flightCount, flights) = sys.ShowDepartureFlightsChronological();
                            Console.WriteLine(msg);
                            Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");

                            // Use of helper function Read().
                            f = Read();

                            // Ensures user selects valid available flight.
                            while (!int.TryParse(f, out int flightNum) || flightNum < 1 || flightNum > flightCount)
                            {
                                Console.WriteLine("#####\n# Error - Supplied value is out of range.\n# Please try again.\n#####");
                                Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");
                                f = Read();
                            }

                            selectedDepartureFlight = flights[int.Parse(f) - 1];

                            // If the traveller has a pre-existing arrival booking, this ensures that the departure flight is scheduled to depart 
                            // after the arrival of the arrival flight. 
                            if (ff.ArrivalBooking != null)
                            {
                                // Use of GetFlightByPlaneId() helper function in AirportSystem instance sys.
                                var arrivalFlight = sys.GetFlightByPlaneId(ff.ArrivalBooking.FlightPlaneId);
                                if (selectedDepartureFlight.Scheduled <= arrivalFlight.Scheduled)
                                {
                                    Console.WriteLine("#####\n# Error - The departure time must be after the arrival time.\n# Please try again.\n#####");
                                    continue;
                                }
                            }

                            // Exit while loop once valid flight has been selected.
                            validFlightSelected = true;
                        }

                        // Once valid flight is selected, proceed with seat bookings.
                        Console.WriteLine("Please enter in your seat row between 1 and 10:");

                        // Use of helper function Read().
                        var seat_r = Read();

                        // While loop validating that the selected seat row is greater than one and less than ten.
                        while (Convert.ToInt32(seat_r) < 1 || Convert.ToInt32(seat_r) > 10)
                        {
                            Console.WriteLine("#####\n# Error - Supplied seat row is invalid.\n# Please try again.\n#####");
                            Console.WriteLine("Please enter in your seat row between 1 and 10:");
                            seat_r = Read();
                        }

                        Console.WriteLine("Please enter in your seat column between A and D:");

                        // Use of helper function Read().
                        var seat_c = Read();

                        // Use of Validators to validate user input for the selected seat column.
                        while (!Validators.ValidSeatColumn(seat_c))
                        {
                            Console.WriteLine("#####\n# Error - Supplied seat column is invalid.\n# Please try again.\n#####");
                            Console.WriteLine("Please enter in your seat column between A and D:");
                            seat_c = Read();
                        }

                        string seat = seat_r + seat_c;

                        // Use of helper function BookFlight() from AirportSystem instance sys to book seat on flight for traveller.
                        var result = sys.BookFlight(ff.Email, int.Parse(f), seat, false);
                        Console.WriteLine(result.msg);
                    }
                }

                // See all booked flight details as a frequent flyer details.
                else if (c == "5")
                {
                    Console.WriteLine($"Showing flight details for {ff.Name}:");

                    // If an arrival flight booking exists for the traveller, return the flight details as a formatted string.
                    if (ff.ArrivalBooking != null)
                    {
                        // Use of GetFlightByPlaneId() helper function in the AirportSystem instance sys.
                        var arrivalFlight = sys.GetFlightByPlaneId(ff.ArrivalBooking.FlightPlaneId);
                        if (arrivalFlight is ArrivalFlight af)
                        {
                            string formattedSeat = $"{ff.ArrivalBooking.Seat[..^1]}:{ff.ArrivalBooking.Seat[^1]}";
                            Console.WriteLine($"Arrival Flight: Flight {arrivalFlight.Airline}{arrivalFlight.FlightCode} from {af.DepartureCity} arriving at {arrivalFlight.Scheduled:HH:mm dd/MM/yyyy} in seat {formattedSeat}.");
                        }
                    }

                    // If a departure flight booking exists for the traveller, return the flight details as a formatted string.
                    if (ff.DepartureBooking != null)
                    {
                        // Use of GetFlightByPlaneId() helper function in the AirportSystem instance sys.
                        var departureFlight = sys.GetFlightByPlaneId(ff.DepartureBooking.FlightPlaneId);
                        if (departureFlight is DepartureFlight df)
                        {
                            string formattedSeat = $"{ff.DepartureBooking.Seat[..^1]}:{ff.DepartureBooking.Seat[^1]}";
                            Console.WriteLine($"Departure Flight: Flight {departureFlight.Airline}{departureFlight.FlightCode} to {df.ArrivalCity} departing at {departureFlight.Scheduled:HH:mm dd/MM/yyyy} in seat {formattedSeat}.");
                        }
                    }
                }

                // See number of frequent flyer points as a frequent flyer.
                else if (c == "6")
                {
                    // If loop dictating number and display of frequent flyer points based on currently booked flights, if any.

                    // If there are no arrival or departure flights booked. 
                    if (ff.ArrivalBooking == null && ff.DepartureBooking == null)
                    {
                        // Use of DisplayFrequentFlyerPoints() helper function from FrequentFlyer instance ff.
                        Console.WriteLine(ff.DisplayFrequentFlyerPoints());
                    }

                    // If there are arrival flights but not departure flights.
                    else if (ff.ArrivalBooking != null && ff.DepartureBooking == null)
                    {
                        // Use of GetFlightByPlaneId() helper function from the AirportSystem instance sys.
                        var arrivalFlight = sys.GetFlightByPlaneId(ff.ArrivalBooking.FlightPlaneId);
                        int ffPoints1 = ff.Points;

                        // If there is an arrival flight booked, sum up the total frequent flyer points and display the corresponding message.
                        if (arrivalFlight is ArrivalFlight af)
                        {
                            // Use of GetCityPoints() helper function from AirportSystem.
                            int cityFFPoints = AirportSystem.GetCityPoints(af.DepartureCity);

                            // Use of DisplayFrequentFlyerPoints() from the FrequentFlyer instance ff. 
                            Console.WriteLine(ff.DisplayFrequentFlyerPoints());
                            Console.WriteLine($"Your points from your arrival flight will be : {cityFFPoints.ToString("N0")}.");
                            Console.WriteLine($"After completing your flight your new points will be: {(ffPoints1 + cityFFPoints).ToString("N0")}.");
                        }
                    }

                    // If there are departure flights but not arrival flights.
                    else if (ff.ArrivalBooking == null && ff.DepartureBooking != null)
                    {
                        // Use of GetFlightByPlaneId() helper function from the AirportSystem instance sys.
                        var departureFlight = sys.GetFlightByPlaneId(ff.DepartureBooking.FlightPlaneId);
                        int ffPoints2 = ff.Points;

                        // If there is a departure flight booked, sum up the total frequent flyer points and display the corresponding message.
                        if (departureFlight is DepartureFlight df)
                        {
                            // Use of GetCityPoints() helper function from AirportSystem.
                            int cityFFPoints = AirportSystem.GetCityPoints(df.ArrivalCity);

                            // Use of DisplayFrequentFlyerPoints() from the FrequentFlyer instance ff.
                            Console.WriteLine(ff.DisplayFrequentFlyerPoints());
                            Console.WriteLine($"Your points from your departure flight will be : {cityFFPoints.ToString("N0")}.");
                            Console.WriteLine($"After completing your flight your new points will be: {(ffPoints2 + cityFFPoints).ToString("N0")}.");
                        }
                    }

                    // If there are both arrival and departure flights.
                    else if (ff.ArrivalBooking != null && ff.DepartureBooking != null) // both arrivals and departures
                    {
                        // Use of GetFlightByPlaneId() helper function from the AirportSystem instance sys.
                        var arrivalFlight = sys.GetFlightByPlaneId(ff.ArrivalBooking.FlightPlaneId);
                        var departureFlight = sys.GetFlightByPlaneId(ff.DepartureBooking.FlightPlaneId);

                        int ffPoints3 = ff.Points;
                        int cityFFPoints_arr = 0;
                        int cityFFPoints_dep = 0;

                        // Separately sum up the frequent flyer points from arrival flights and departure flights.
                        if (arrivalFlight is ArrivalFlight af)
                        {
                            cityFFPoints_arr = AirportSystem.GetCityPoints(af.DepartureCity);
                            ffPoints3 += cityFFPoints_arr;
                        }
                        if (departureFlight is DepartureFlight df)
                        {
                            cityFFPoints_dep = AirportSystem.GetCityPoints(df.ArrivalCity);
                            ffPoints3 += cityFFPoints_dep;
                        }

                        // Use of DisplayFrequentFlyerPoints() from FrequentFlyer instance ff to display relevant display message.
                        Console.WriteLine(ff.DisplayFrequentFlyerPoints());
                        Console.WriteLine($"Your points from your arrival flight will be : {cityFFPoints_arr.ToString("N0")}.");
                        Console.WriteLine($"Your points from your departure flight will be: {cityFFPoints_dep.ToString("N0")}.");
                        Console.WriteLine($"After completing your flights your new points will be: {ffPoints3.ToString("N0")}.");
                    }
                }

                // Log out of frequent flyer menu.
                else if (c == "7")
                {
                    return;
                }
                else Console.WriteLine("Invalid choice");
            }
        }

        /// <summary>
        /// Creates a new arrival flight entry within the Brisbane Airport App.
        /// RESPONSIBILITY: Prompts the user to input details for a new arrival flight, validates all provided values,
        /// and submits the data to the system for registration.
        /// </summary>
        static void CreateArr()
        {
            string airline_name = "";
            string airline_code = "";
            Console.WriteLine("Please enter the airline:\n1. Jetstar\n2. Qantas\n3. Regional Express\n4. Virgin\n5. Fly Pelican");
            Console.WriteLine("Please enter a choice between 1 and 5:");

            // Use of helper function Read().
            var al = Read();

            // Assign corresponding airline name and code based on user input.
            if (al == "1") { airline_name = "Jetstar"; airline_code = "JST"; }
            else if (al == "2") { airline_name = "Qantas"; airline_code = "QFA"; }
            else if (al == "3") { airline_name = "Regional Express"; airline_code = "RXA"; }
            else if (al == "4") { airline_name = "Virgin"; airline_code = "VOZ"; }
            else if (al == "5") { airline_name = "Fly Pelican"; airline_code = "FRE"; }
            else
            {
                Console.WriteLine("Invalid choice 1");
                Console.WriteLine("Please enter a choice between 1 and 5");
                al = Read();
            }

            string departure_city = "";
            Console.WriteLine("Please enter the departing city:\n1. Sydney\n2. Melbourne\n3. Rockhampton\n4. Adelaide\n5. Perth");
            Console.WriteLine("Please enter a choice between 1 and 5:");

            // Use of helper function Read().
            var dc = Read();

            // Assign corresponding departure city name based on user input.
            if (dc == "1") { departure_city = "Sydney"; }
            else if (dc == "2") { departure_city = "Melbourne"; }
            else if (dc == "3") { departure_city = "Rockhampton"; }
            else if (dc == "4") { departure_city = "Adelaide"; }
            else if (dc == "5") { departure_city = "Perth"; }
            else
            {
                Console.WriteLine("Invalid choice 2");
                Console.WriteLine("Please enter a choice between 1 and 5");
                dc = Read();
            }

            Console.WriteLine("Please enter in your flight id between 100 and 900:");
            
            // Use of helper function Read().
            var fid = Read();

            // Use of Validators to validate user input for flight ID and display corresponding error message.
            while (!Validators.ValidFlightId(Convert.ToInt32(fid)))
            {
                Console.WriteLine("Invalid flight id");
                Console.WriteLine("Please enter in your flight id between 100 and 900:");
                fid = Read();
            }

            Console.WriteLine("Please enter in your plane id between 0 and 9:");

            // Use of helper function Read().
            var pid = Read();

            // Use of Validators to validate user input for plane ID and display corresponding error message.
            while (!Validators.ValidPlaneId(Convert.ToInt32(pid)))
            {
                Console.WriteLine("Invalid plane id");
                Console.WriteLine("Please enter in your plane id between 0 and 9:");
                pid = Read();
            }

            Console.WriteLine("Please enter in the arrival date and time in the format HH:mm dd/MM/yyyy:");
            
            // Use of helper function Read().
            var dt = Read();

            // Use of Validators to validate user input for date and time and display corresponding error message.
            while (!Validators.ValidDateTime(dt))
            {
                Console.WriteLine("Invalid arrival date and time or invalid format");
                Console.WriteLine("Please enter in the arrival date and time in the format HH:mm dd/MM/yyyy:");
                dt = Read();
            }

            Console.WriteLine(sys.CreateArrivalFlight(airline_code, fid, departure_city, pid, DateTime.ParseExact(dt, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture), airline_name));
        }

        /// <summary>
        /// Creates a new departure flight entry within the Brisbane Airport App.
        /// RESPONSIBILITY: Prompts the user to input details for a new departure flight, validates all provided values,
        /// and submits the data to the system for registration.
        /// </summary>
        static void CreateDep()
        {
            string airline_name = "";
            string airline_code = "";
            Console.WriteLine("Please enter the airline:\n1. Jetstar\n2. Qantas\n3. Regional Express\n4. Virgin\n5. Fly Pelican");
            Console.WriteLine("Please enter a choice between 1 and 5:");

            // Use of helper function Read().
            var al = Read();

            // Assign corresponding airline name and code based on user input.
            if (al == "1") { airline_name = "Jetstar"; airline_code = "JST"; }
            else if (al == "2") { airline_name = "Qantas"; airline_code = "QFA"; }
            else if (al == "3") { airline_name = "Regional Express"; airline_code = "RXA"; }
            else if (al == "4") { airline_name = "Virgin"; airline_code = "VOZ"; }
            else if (al == "5") { airline_name = "Fly Pelican"; airline_code = "FRE"; }
            else
            {
                Console.WriteLine("Invalid choice 1");
                Console.WriteLine("Please enter a choice between 1 and 5");
                al = Read();
            }

            string arrival_city = "";
            Console.WriteLine("Please enter the arrival city:\n1. Sydney\n2. Melbourne\n3. Rockhampton\n4. Adelaide\n5. Perth");
            Console.WriteLine("Please enter a choice between 1 and 5:");

            // Use of helper function Read().
            var dc = Read();

            // Assign corresponding arrival city name based on user input.
            if (dc == "1") { arrival_city = "Sydney"; }
            else if (dc == "2") { arrival_city = "Melbourne"; }
            else if (dc == "3") { arrival_city = "Rockhampton"; }
            else if (dc == "4") { arrival_city = "Adelaide"; }
            else if (dc == "5") { arrival_city = "Perth"; }
            else
            {
                Console.WriteLine("Invalid choice 2");
                Console.WriteLine("Please enter a choice between 1 and 5");
                dc = Read();
            }

            Console.WriteLine("Please enter in your flight id between 100 and 900:");
            
            // Use of helper function Read().
            var fid = Read();

            // Use of Validators to validate user input for flight ID and display corresponding error message.
            while (!Validators.ValidFlightId(Convert.ToInt32(fid)))
            {
                Console.WriteLine("Invalid flight id");
                Console.WriteLine("Please enter in your flight id between 100 and 900:");
                fid = Read();
            }

            Console.WriteLine("Please enter in your plane id between 0 and 9:");
            
            // Use of helper function Read().
            var pid = Read();

            // Use of Validators to validate user input for plane ID and display corresponding error message.
            while (!Validators.ValidPlaneId(Convert.ToInt32(pid)))
            {
                Console.WriteLine("Invalid plane id");
                Console.WriteLine("Please enter in your plane id between 0 and 9:");
                pid = Read();
            }

            Console.WriteLine("Please enter in the departure date and time in the format HH:mm dd/MM/yyyy:");
            
            // Use of helper function Read().
            var dt = Read();

            // Use of Validators to validate user input for date and time and display corresponding error message.
            while (!Validators.ValidDateTime(dt))
            {
                Console.WriteLine("Invalid arrival date and time or invalid format");
                Console.WriteLine("Please enter in the arrival date and time in the format HH:mm dd/MM/yyyy:");
                dt = Read();
            }

            Console.WriteLine(sys.CreateDepartureFlight(airline_code, fid, arrival_city, pid, DateTime.ParseExact(dt, "HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture), airline_name));
        }

        /// <summary>
        /// Delays a selected arrival flight by a user-specified number of minutes.
        /// RESPONSIBILITY: Displays all registered arrival flights in chronological order, 
        /// allows the user to select one and applies a delay adjustment to its schedule.
        /// </summary>
        static void DelayArrivalFlight()
        {
            // Use of ShowArrivalFlightsChronological() helper function in AirportSystem instance sys.
            var (msg, flightCount, flights) = sys.ShowArrivalFlightsChronological();

            // If there are no flights to delay, display corresponding error message and return to menu.
            if (flightCount == 0)
            {
                Console.WriteLine("The airport does not have any arrival flights.");
                return;
            }

            Console.WriteLine("Please enter the arrival flight:");
            Console.WriteLine(msg);
            Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");

            // Use of helper function Read().
            var f = Read();

            // Ensures user selects valid available flight.
            while (!int.TryParse(f, out int flightNum) || (flightNum < 1) || flightNum > flightCount)
            {
                Console.WriteLine("#####\n# Error - Supplied value is out of range.\n# Please try again.\n#####");
                Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");
                f = Read();
            }

            var selectedFlight = flights[int.Parse(f) - 1];

            Console.WriteLine("Please enter in your minutes delayed:");

            // Use of helper function Read().
            var d = Read();

            // Ensures user enters valid number of minutes to delay flight by.
            while (!int.TryParse(d, out int minutes) || minutes <= 0)
            {
                Console.WriteLine("#####\n# Error - Invalid delay time.\n Please try again.\n#####");
                Console.WriteLine("Please enter in your minutes delayed:");
                d = Read();
            }

            // Use of DelayFlightAndAdjust() helper function in the AirportSystem instance sys 
            // to delay flight and adjust accordingly.
            sys.DelayFlightAndAdjust(selectedFlight.PlaneId, TimeSpan.FromMinutes(int.Parse(d)));
        }

        /// <summary>
        /// Delays a selected departure flight by a user-specified number of minutes.
        /// RESPONSIBILITY: Displays all registers departure flights in chronological order,
        /// allows the user to select one and applies a delay adjustment to its schedule.
        /// </summary>
        static void DelayDepartureFlight()
        {
            // Use of ShowDepartureFlightsChronological() helper function in AirportSystem instance sys.
            var (msg, flightCount, flights) = sys.ShowDepartureFlightsChronological();

            // // If there are no flights to delay, display corresponding error message and return to menu.
            if (flightCount == 0)
            {
                Console.WriteLine("The airport does not have any departure flights.");
                return;
            }

            Console.WriteLine("Please enter the departure flight:");
            Console.WriteLine(msg);
            Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");

            // Use of helper function Read().
            var f = Read();

            // Ensures user selects valid available flight.
            while (!int.TryParse(f, out int flightNum) || (flightNum < 1) || flightNum > flightCount)
            {
                Console.WriteLine("#####\n# Error - Supplied value is out of range.\n# Please try again.\n#####");
                Console.WriteLine($"Please enter a choice between 1 and {flightCount}:");
                f = Read();
            }

            var selectedFlight = flights[int.Parse(f) - 1];

            Console.WriteLine("Please enter in your minutes delayed:");

            // Use of helper function Read().
            var d = Read();

            // Ensures user enters valid number of minutes to delay flight by.
            while (!int.TryParse(d, out int minutes) || minutes <= 0)
            {
                Console.WriteLine("#####\n# Error - Invalid delay time.\n Please try again.\n#####");
                Console.WriteLine("Please enter in your minutes delayed:");
                d = Read();
            }

            // Use of DelayFlightAndAdjust() helper function in the AirportSystem instance sys 
            // to delay flight and adjust accordingly.
            sys.DelayFlightAndAdjust(selectedFlight.PlaneId, TimeSpan.FromMinutes(int.Parse(d)));
        }
    }
}