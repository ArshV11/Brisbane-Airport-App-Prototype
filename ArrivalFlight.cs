using System;

namespace BrisbaneAirportApp
{
    /// <summary>
    /// Represents a flight arriving at Brisbane Airport.
    /// RESPONSIBILITY: Stores information specific to arrival flights, including the city of departure and standard flight details inherited from the parent class, Flight.
    /// </summary>
    public class ArrivalFlight : Flight
    {
        /// <summary>
        /// Gets the city from which this arrival flight originates.
        /// </summary>
        public string DepartureCity { get; }

        /// <summary>
        /// Initialises a new instance of the ArrivalFlight class.
        /// </summary>
        /// <param name="flightCode">The flight code identifying the flight (e.g. "150").</param>
        /// <param name="airline">The airline code for the airline operating this flight (e.g. "JST" for "Jetstar").</param>
        /// <param name="planeId">The unique plane identifier for the flight.</param>
        /// <param name="scheduled">The scheduled date and time of arrival in the format HH:mm dd/MM/yyyy.</param>
        /// <param name="departureCity">The name of city from which the flight departs.</param>
        public ArrivalFlight(string flightCode, Airline airline, string planeId, DateTime scheduled, string departureCity) : base(flightCode, airline, planeId, scheduled) => DepartureCity = departureCity;
    }
}