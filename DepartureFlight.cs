using System;

namespace BrisbaneAirportApp
{
    /// <summary>
    /// Represents a flight departing from Brisbane Airport.
    /// RESPONSIBILITY: Stores information specific to departure flights, including the arrival city and standard flight details inherited from the parent class, Flight. 
    /// </summary>
    public class DepartureFlight : Flight
    {
        /// <summary>
        /// Gets the arrival city for this departure flight
        /// </summary>
        public string ArrivalCity { get; }
        
        /// <summary>
        /// Initialises a new instance of the DepartureFlight class.
        /// </summary>
        /// <param name="flightCode">The flight code identifying the flight (e.g. "150").</param>
        /// <param name="airline">The airline code for the airline operating this flight (e.g. "JST" for "Jetstar").</param>
        /// <param name="planeId">The unique plane identifier for the flight.</param>
        /// <param name="scheduled">The scheduled date and time of departure in the format HH:mm dd/MM/yyyy.</param>
        /// <param name="departureCity">The name of city at which the flight arrives.</param>
        public DepartureFlight(string flightCode, Airline airline, string planeId, DateTime scheduled, string arrivalCity) : base(flightCode, airline, planeId, scheduled) => ArrivalCity = arrivalCity;
    }
}