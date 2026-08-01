namespace BrisbaneAirportApp
{
    /// <summary>
    /// Represents a flight manager with administrative access to flight operations.
    /// RESPONSIBILITY: Extends parent class, User, to include staff credentials for flight management.
    /// </summary>
    public class FlightManager : User
    {
        /// <summary>
        /// Gets the unique staff ID assigned to this flight manager.
        /// </summary>
        public string StaffId { get; }
        
        /// <summary>
        /// Initialises a new instance of the FlightManager class.
        /// </summary>
        /// <param name="name">The flight manager's name.</param>
        /// <param name="age">The flight manager's age.</param>
        /// <param name="mobile">The flight manager's mobile phone number.</param>
        /// <param name="email">The flight manager's email address.</param>
        /// <param name="password">The flight manager's password.</param>
        /// <param name="staffId">The flight manager's unique staff ID.</param>
        public FlightManager(string name, int age, string mobile, string email, string password, string staffId)
            : base(name, age, mobile, email, password) => StaffId = staffId;

        /// <summary>
        /// Displays the flight managers's profile information, including staff ID, as a formatted string.
        /// </summary>
        /// <returns>The flight manager's profile information as formatted string</returns>
        public override string DisplayInfo() => base.DisplayInfo() + $"\nStaff ID: {StaffId}";
    }
}