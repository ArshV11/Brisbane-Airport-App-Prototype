namespace BrisbaneAirportApp
{
    /// <summary>
    /// Manages users, flights and bookings within the Brisbane Airport App system.
    /// RESPONSIBILITY: Coordinates user registration, authentication, flight creation, 
    /// seat booking and scheduling operations across all user and flight types.
    /// </summary>
    public class AirportSystem
    {
        /// <summary>
        /// A collection of all registered users in the system, indexed by email address. 
        /// </summary>
        private readonly Dictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// A collection of all flights registered in the system, indexed by their plane ID.
        /// </summary>
        private readonly Dictionary<string, Flight> _flights = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// A static dictionary that stores the point values assigned to specific cities.
        /// </summary>
        public static readonly Dictionary<string, int> CityPoints = new(StringComparer.OrdinalIgnoreCase)
        { ["Sydney"] = 1200, ["Melbourne"] = 1750, ["Rockhampton"] = 1400, ["Adelaide"] = 1950, ["Perth"] = 3375 };

        /// <summary>
        /// Retrieves the frequent flyer points associated with a given city.
        /// </summary>
        /// <param name="city">The name of the city to look up.</param>
        /// <returns>The number of points assigned to the city or 0, if not found.</returns>
        public static int GetCityPoints(string city) => CityPoints.TryGetValue(city, out int points) ? points : 0;

        /// <summary>
        /// Determines whether a given email address is already registered in the system.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True, if the email exists, or False, if the email does not.</returns>
        public bool EmailExists(string email) => _users.ContainsKey(email);

        /// <summary>
        /// Checks if there are any registered users in the system.
        /// </summary>
        /// <returns>TRUE, if one or more users exist, or FALSE, if no users exist.</returns>
        public bool HasUsers() => _users.Count > 0;

        /// <summary>
        /// Authenticates a user based on their email and password.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The authenticated User, if credentials are valid, or NULL, if credentials are invalid.</returns>
        public User Login(string email, string password) => _users.TryGetValue(email, out var u) && u.Authenticate(password) ? u : null;
        
        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The matching User instance, if found, or NULL, if not found.</returns>
        public User GetUserByEmail(string email) => _users.TryGetValue(email, out var u) ? u : null;

        /// <summary>
        /// Registers a new traveller in the system.
        /// </summary>
        /// <param name="name">The traveller's name.</param>
        /// <param name="age">The traveller's age.</param>
        /// <param name="mobile">The traveller's mobile.</param>
        /// <param name="email">The traveller's email.</param>
        /// <param name="password">The traveller's password.</param>
        /// <returns>A message indicating the outcome of the registration attempt.</returns>
        public string RegisterTraveller(string name, int age, string mobile, string email, string password)
        {
            // Prevents the same email address being used by multiple users.
            if (_users.ContainsKey(email))
            {
                return ("Email already in use");
            }
            else
            {
                _users[email] = new Traveller(name, age, mobile, email, password);
                return ($"Congratulations {name}. You have registered as a traveller.");
            }
        }

        /// <summary>
        /// Registers a new frequent flyer in the system.
        /// </summary>
        /// <param name="name">The frequent flyer's name.</param>
        /// <param name="age">The frequent flyers's age.<param>
        /// <param name="mobile">The frequent flyers's mobile.</param>
        /// <param name="email">The frequent flyers's email.</param>
        /// <param name="password">The frequent flyers's password.</param>
        /// <param name="ffNumber">The frequent flyer number associated with the frequent flyer.</param>
        /// <param name="ffPoints">The frequent flyers's initial number of frequent flyer points.</param>
        /// <returns>A message indicating whether registration succeeded or failed.</returns>
        public string RegisterFrequentFlyer(string name, int age, string mobile, string email, string password, int ffNumber, int ffPoints)
        {
            // Prevents the same email address from being used by multiple users.
            if (_users.ContainsKey(email))
            {
                return ("Email already in use");
            }
            else
            {
                _users[email] = new FrequentFlyer(name, age, mobile, email, password, ffNumber, ffPoints);
                return ($"Congratulations {name}. You have registered as a frequent flyer.");
            }
        }

        /// <summary>
        /// Registers a new flight manager in the system.
        /// </summary>
        /// <param name="name">The flight manager's name.</param>
        /// <param name="age">The flight manager's age.</param>
        /// <param name="mobile">The flight manager's mobile.</param>
        /// <param name="email">The flight manager's email.</param>
        /// <param name="password">The flight manager's password.</param>
        /// <param name="staffId">The flight manager's staff ID.</param>
        /// <returns>A message indicating whether registration succeeded or failed.</returns>
        public string RegisterFlightManager(string name, int age, string mobile, string email, string password, string staffId)
        {
            // Prevents the same email address from being used by multiple users.
            if (_users.ContainsKey(email))
            {
                return ("Email already in use");
            }
            else
            {
                _users[email] = new FlightManager(name, age, mobile, email, password, staffId);
                return ($"Congratulations {name}. You have registered as a flight manager.");
            }
        }

        /// <summary>
        /// Creates and registers a new arrival flight.
        /// </summary>
        /// <param name="airlineCode">The airline code (e.g. "JST").</param>
        /// <param name="flightCode">The flight code (e.g. "150")</param>
        /// <param name="departureCity">The city from which the flight departs.</param>
        /// <param name="planeId">The plane identifier.</param>
        /// <param name="scheduled">The scheduled arrival date and time in the format HH:mm dd/MM/yyyy.</param>
        /// <param name="airlineName">The airline's full name (e.g. "Jetstar").</param>
        /// <returns>A message indicating whether flight creation succeeded or failed.</returns>
        public string CreateArrivalFlight(string airlineCode, string flightCode, string departureCity, string planeId, DateTime scheduled, string airlineName)
        {
            // Ensures valid airline is being entered.
            if (!Enum.TryParse<Airline>(airlineCode, out var airline))
            {
                return ("Invalid airline");
            }
            
            string fullPlaneId = $"{planeId}A";

            // Prevents the same plane from being used for multiple arrival flights at the same time.
            if (_flights.ContainsKey(fullPlaneId))
            {
                return ($"#####\n# Error - Plane {airline}{fullPlaneId} has already been assigned to an arrival flight.\n#####\n");
            }

            _flights[fullPlaneId] = new ArrivalFlight(flightCode, airline, fullPlaneId, scheduled, departureCity);
            return ($"Flight {airlineCode}{flightCode} on plane {airline}{fullPlaneId} has been added to the system.\n");
        }

        /// <summary>
        /// Creates and registers a new departure flight.
        /// </summary>
        /// <param name="airlineCode">The airline code (e.g. "JST").</param>
        /// <param name="flightCode">The flight code (e.g. "150")</param>
        /// <param name="arrivalCity">The city at which the flight arrives.</param>
        /// <param name="planeId">The plane identifier.</param>
        /// <param name="scheduled">The scheduled departure date and time in the format HH:mm dd/MM/yyyy.</param>
        /// <param name="airlineName">The airline's full name (e.g. "Jetstar").</param>
        /// <returns>A message indicating whether flight creation succeeded or failed.</returns>
        public string CreateDepartureFlight(string airlineCode, string flightCode, string arrivalCity, string planeId, DateTime scheduled, string airlineName)
        {
            // Ensures valid airline is being entered.
            if (!Enum.TryParse<Airline>(airlineCode, out var airline))
            {
                return ("Invalid airline");
            }

            string fullPlaneId = $"{planeId}D";

            // Prevents the same plane from being used for multiple departure flights at the same time.
            if (_flights.ContainsKey(fullPlaneId))
            {
                return ($"#####\n# Error - Plane {airline}{fullPlaneId} has already been assigned to a departure flight.\n#####\n");
            }

            _flights[fullPlaneId] = new DepartureFlight(flightCode, airline, fullPlaneId, scheduled, arrivalCity);
            return ($"Flight {airlineCode}{flightCode} on plane {airline}{fullPlaneId} has been added to the system.\n");
        }

        /// <summary>
        /// Lists all flights in chronological order
        /// </summary>
        /// <returns>An ordered collection of all flights by their scheduled time.</returns>
        public IEnumerable<Flight> ListFlightsChronological() => _flights.Values.OrderBy(f => f.Scheduled);

        /// <summary>
        /// Retrieves a flight by its plane ID.
        /// </summary>
        /// <param name="planeId">The plane identifier.</param>
        /// <returns>The matching Flight, if found, or NULL, if not found.</returns>
        public Flight GetFlightByPlaneId(string planeId) => _flights.TryGetValue(planeId, out var f) ? f : null;

        /// <summary>
        /// Displays all arrival and departure flights in chronological order.
        /// </summary>
        public void ShowAllFlightsChronological()
        {
            var allFlights = ListFlightsChronological().ToList();
            var arrivals = allFlights.OfType<ArrivalFlight>().ToList();
            var departures = allFlights.OfType<DepartureFlight>().ToList();

            // If loop controlling the arrival flights message.
            Console.WriteLine("\nArrival Flights:");
            if (arrivals.Count == 0)
            {
                Console.WriteLine("There are no arrival flights.");
            }
            else
            {
                foreach (var f in arrivals)
                {
                    // Determines the airline name ("Jetstar") based on the provided airline ("JST").
                    var airlineName = f.Airline switch
                    {
                        Airline.JST => "Jetstar",
                        Airline.QFA => "Qantas",
                        Airline.RXA => "Regional Express",
                        Airline.VOZ => "Virgin",
                        Airline.FRE => "Fly Pelican",
                        _ => f.Airline.ToString()
                    };

                    Console.WriteLine($"Flight {f.Airline}{f.FlightCode} operated by {airlineName} arriving at {f.Scheduled:HH:mm dd/MM/yyyy} from {f.DepartureCity} on plane {f.Airline}{f.PlaneId}.");
                }
            }

            // If loop controlling the departure flights message
            Console.WriteLine("Departure Flights:");
            if (departures.Count == 0)
            {
                Console.WriteLine("There are no departure flights.\n");
            }
            else
            {
                foreach (var f in departures)
                {
                    // Determines the airline name ("Jetstar") based on the provided airline ("JST").
                    var airlineName = f.Airline switch
                    {
                        Airline.JST => "Jetstar",
                        Airline.QFA => "Qantas",
                        Airline.RXA => "Regional Express",
                        Airline.VOZ => "Virgin",
                        Airline.FRE => "Fly Pelican",
                        _ => f.Airline.ToString()
                    };
                    Console.WriteLine($"Flight {f.Airline}{f.FlightCode} operated by {airlineName} departing at {f.Scheduled:HH:mm dd/MM/yyyy} to {f.ArrivalCity} on plane {f.Airline}{f.PlaneId}.");
                }
            }
        }

        /// <summary>
        /// Displays only all arrival flights in chronological order based on their scheduled times.
        /// </summary>
        /// <returns>A tuple containing:
        ///     msg: A formatted string representation of all arrival flights.
        ///     fc: The total number of arrival flights as an integer.
        ///     flights: The list of ArrivalFlight objects.
        /// returns>
        public (string msg, int fc, List<ArrivalFlight> flights) ShowArrivalFlightsChronological()
        {
            var allFlights = ListFlightsChronological().ToList();
            var arrivals = allFlights.OfType<ArrivalFlight>().ToList();
            
            // Initialise return message and initial number of arrival flights.
            int flight_count = 0;
            string msg = "";

            if (arrivals.Count == 0)
            {
                return ("There are no arrival flights", 0, arrivals);
            }

            foreach (var f in arrivals)
            {
                flight_count += 1;

                // Determines the airline name ("Jetstar") based on the provided airline ("JST").
                var airlineName = f.Airline switch
                {
                    Airline.JST => "Jetstar",
                    Airline.QFA => "Qantas",
                    Airline.RXA => "Regional Express",
                    Airline.VOZ => "Virgin",
                    Airline.FRE => "Fly Pelican",
                    _ => f.Airline.ToString()
                };

                // Append each flight details to the return message.
                msg += $"{flight_count}. Flight {f.Airline}{f.FlightCode} operated by {airlineName} arriving at {f.Scheduled:HH:mm dd/MM/yyyy} from {f.DepartureCity} on plane {f.Airline}{f.PlaneId}.\n";
            }

            msg = msg.TrimEnd('\n');
            return (msg, flight_count, arrivals);
        }

        /// <summary>
        /// Displays only all departure flights in chronological order based on their scheduled times.
        /// </summary>
        /// <returns>A tuple containing:
        ///     msg: A formatted string representation of all departure flights.
        ///     fc: The total number of departure flights as an integer.
        ///     flights: The list of DepartureFlight objects.
        /// returns>
        public (string msg, int fc, List<DepartureFlight> flights) ShowDepartureFlightsChronological()
        {
            var allFlights = ListFlightsChronological().ToList();
            var departures = allFlights.OfType<DepartureFlight>().ToList();

            // Initialise return message and initial number of arrival flights.
            int flight_count = 0;
            string msg = "";

            if (departures.Count == 0)
            {
                return ("There are no arrival flights", 0, departures);
            }

            foreach (var f in departures)
            {
                flight_count += 1;

                // Determines the airline name ("Jetstar") based on the provided airline ("JST").
                var airlineName = f.Airline switch
                {
                    Airline.JST => "Jetstar",
                    Airline.QFA => "Qantas",
                    Airline.RXA => "Regional Express",
                    Airline.VOZ => "Virgin",
                    Airline.FRE => "Fly Pelican",
                    _ => f.Airline.ToString()
                };

                // Append each flight details to the return message.
                msg += $"{flight_count}. Flight {f.Airline}{f.FlightCode} operated by {airlineName} departing at {f.Scheduled:HH:mm dd/MM/yyyy} to {f.ArrivalCity} on plane {f.Airline}{f.PlaneId}.\n";
            }

            msg = msg.TrimEnd('\n');
            return (msg, flight_count, departures);
        }

        /// <summary>
        /// Attempts to book a flight for a user based on their email, selected flight and seat choice.
        /// </summary>
        /// <param name="userEmail">The email address of the user requesting the booking.</param>
        /// <param name="flightNumber">The numerical index of the flight as displayed in the chronological list provided during booking.</param>
        /// <param name="seat">The seat code to be booked (e.g. "12A").</param>
        /// <param name="IsArrival">Boolean value specifying whether the booking is for an arrival flight.</param>
        /// <returns>A tuple containing:
        ///     ok: Indicates whether the booking was successful or not.
        ///     msg: A descriptive message detailing the booking result or any error encountered.
        /// </returns>
        public (bool ok, string msg) BookFlight(string userEmail, int flightNumber, string seat, bool IsArrival)
        {
            // Ensure valid user is being entered.
            if (!_users.TryGetValue(userEmail, out var user)) return (false, "No such user");

            var allFlights = ListFlightsChronological().ToList();

            // Filter flights based on whether the user is booking an arrival flight or not.
            List<Flight> selectedFlights;
            if (IsArrival)
            {
                selectedFlights = allFlights.OfType<ArrivalFlight>().Cast<Flight>().ToList();
            }
            else
            {
                selectedFlights = allFlights.OfType<DepartureFlight>().Cast<Flight>().ToList();
            }

            // Ensures valid numerical index has been entered.
            if (flightNumber < 1 || flightNumber > selectedFlights.Count)
                return (false, $"Invalid flight number {selectedFlights.Count}");

            var flight = selectedFlights[flightNumber - 1];

            // Ensures valid seat has been entered.
            if (!Validators.ValidSeat(seat)) return (false, "Invalid seat");

            // Verify that a traveller (not frequent flyer) is booking an arrival or departure flight
            // and does not yet have an arrival or departure flight booked, respectively. 
            if (user is Traveller t && ((IsArrival && t.ArrivalBooking != null) || (!IsArrival && t.DepartureBooking != null)))
            {
                return (false, $"You already have an {(IsArrival ? "arrival" : "departure")} booking");
            }

            // Enables traveller's to select a seat for their arrival or departure flight
            // and returns a message based on whether it was successful or not.
            string flightBookedMessage = "";
            if (!flight.IsSeatOccupied(seat))
            {
                // Uses helper function TryBookSeat()
                flight.TryBookSeat(seat, userEmail);

                var bk = new Booking(user.Id, flight.PlaneId, seat);

                // Returns message based on whether flight is an arrival flight or departure flight.
                if (user is Traveller tv) { if (IsArrival) tv.ArrivalBooking = bk; else tv.DepartureBooking = bk; }
                if (flight is ArrivalFlight arr_f)
                {
                    flightBookedMessage = $"Congratulations. You have booked flight {flight.Airline}{flight.FlightCode} from {arr_f.DepartureCity} arriving at {flight.Scheduled:HH:mm dd/MM/yyyy} and are seated in {seat[..^1]}:{seat[^1]}.\n";
                    return (true, flightBookedMessage);
                }
                else if (flight is DepartureFlight dep_f)
                {
                    flightBookedMessage = $"Congratulations. You have booked flight {flight.Airline}{flight.FlightCode} to {dep_f.ArrivalCity} departing at {flight.Scheduled:HH:mm dd/MM/yyyy} and are seated in {seat[..^1]}:{seat[^1]}.\n";
                    return (true, flightBookedMessage);
                }
            }

            // Checks the occupancy of a specific seat on a flight and gets the occupant by their email.
            var occ = flight.GetAllBookings().FirstOrDefault(b => b.seat == seat);
            if (occ.email == null)
            {
                return (false, "Seat occupancy inconsistent");
            }
            // Uses helper function GetUserByEmail().
            var occupant = GetUserByEmail(occ.email);

            // If loop controlling for frequent flyers booking available seats and seats occupied by travellers.
            // Frequent flyer bookings are prioritised and travellers are reallocated to the next available seat.
            if (user is FrequentFlyer && occupant is Traveller)
            {
                // Uses helper method ForceAssignSeat().
                flight.ForceAssignSeat(seat, userEmail);

                // Uses helper method _findNextAvailableSeat().
                var reassigned = _findNextAvailableSeat(flight, seat);

                // If there is no available seat, traveller is not reallocated and retains their seat booking.
                if (reassigned == null)
                {
                    // Uses helper method ForceAssignSeat().
                    flight.ForceAssignSeat(seat, occ.email);
                    return (false, "No alternate seat to reassign displaced traveller.");
                }

                // Uses helper method ForceAssignSeat().
                flight.ForceAssignSeat(reassigned, occ.email);

                // If another seat is available, the traveller is reallocated and the 
                // frequent flyer is booked into the seat of their selection. 
                if (user is Traveller tU)
                {
                    if (IsArrival)
                    {
                        tU.ArrivalBooking = new(user.Id, flight.PlaneId, seat);
                    }
                    else
                    {
                        tU.DepartureBooking = new(user.Id, flight.PlaneId, seat);
                    }
                }
                else if (occupant is Traveller t0)
                {
                    if (IsArrival)
                    {
                        t0.ArrivalBooking = new(occupant.Id, flight.PlaneId, reassigned);
                    }
                    else
                    {
                        t0.DepartureBooking = new(occupant.Id, flight.PlaneId, reassigned);
                    }
                }
                else if (user is FrequentFlyer ff2 && flight is DepartureFlight df2 && CityPoints.TryGetValue(df2.ArrivalCity, out var pts2))
                {
                    ff2.AddPoints(pts2);
                }
                else
                {
                    return (true, flightBookedMessage);
                }
            }
            return (false, "Seat already occupied");
        }

        /// <summary>
        /// Finds the next available seat on a given flight starting from a reference seat.
        /// </summary>
        /// <param name="flight">The Flight object on which to search for available seats.</param>
        /// <param name="original">The seat code of the original seat from which to begin the search.</param>
        /// <returns>The seat code of the next available seat, if found, or NULL, if not found.</returns>
        private string _findNextAvailableSeat(Flight flight, string original)
        {
            // Ensures valid seat is being entered.
            if (!Validators.ValidSeat(original))
            {
                return null;
            }
            else
            {
                // Uses nested for loops iterating through each seat via row and column and
                // checks whether seat is available.
                int row = int.Parse(original[..^1]), col = Array.IndexOf(new[] { 'A', 'B', 'C', 'D' }, original[^1]);
                for (int ro = 0; ro < 10; ro++)
                {
                    for (int c = (ro == 0 ? col + 1 : 0); c < 4; c++)
                    {
                        var s = $"{((row - 1 + ro) % 10) + 1}{"ABCD"[c]}";
                        if (!flight.IsSeatOccupied(s))
                        {
                            return s;
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Delays a specified flight and automatically adjusts related flights if applicable.
        /// </summary>
        /// <param name="planeId">The unique identifier of the plane whose flight is to be delayed.</param>
        /// <param name="delay">The time interval in minutes by which to delay the flight.</param>
        /// <returns>A tuple containing:
        ///     ok: Boolean value indicating whether the delay was successfully applied or not.
        ///     msg: A string message summarising the result of the operation.
        /// </returns>
        public (bool ok, string msg) DelayFlightAndAdjust(string planeId, TimeSpan delay)
        {
            // Ensures valid flight has been entered.
            if (!_flights.TryGetValue(planeId, out var flight))
            {
                return (false, "No such flight");
            }

            // Uses helper method in Flight instance "flight".
            flight.DelayBy(delay);

            // If there 
            if (flight is ArrivalFlight && planeId.Length >= 1 && _flights.TryGetValue(planeId[..^1] + "D", out var dep))
            {
                dep.DelayBy(delay);
            }
            return (true, "Flight delayed and related departurues adjusted where applicable.");
        }
    }
}