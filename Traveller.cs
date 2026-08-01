namespace BrisbaneAirportApp
{
    /// <summary>
    /// Represents a standard traveller who can book both arrival and departure flights.
    /// RESPONSIBILITY: Extends the User class to include flight booking information.
    /// </summary>
    public class Traveller : User
    {
        /// <summary>
        /// Gets or sets the traveller's current arrival flight booking.
        /// </summary>
        public Booking ArrivalBooking { get; set; }

        /// <summary>
        /// Gets or sets the traveller's current departure flight booking.
        /// </summary>
        public Booking DepartureBooking { get; set; }

        /// <summary>
        /// Initialises a new instance of the Traveller class.
        /// </summary>
        /// <param name="name">The traveller's name.</param>
        /// <param name="age">The traveller's age.</param>
        /// <param name="mobile">The traveller's mobile phone number.</param>
        /// <param name="email">The traveller's email address.</param>
        /// <param name="password">The traveller's password.</param>
        public Traveller(string name, int age, string mobile, string email, string password) : base(name, age, mobile, email, password) { }
    }
}