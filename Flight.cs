using System;
using System.Collections.Generic;
using System.Linq;

namespace BrisbaneAirportApp
{
    /// <summary>
    /// Represents a base class for a generic flight containing shared information for both arrival and departure flights.
    /// RESPONSIBILITY: Provides core flight attributes and common methods for booking management, 
    /// seat allocation and delay adjustments.
    /// </summary>
    public abstract class Flight
    {
        /// <summary>
        /// Gets the flight code (e.g. "150").
        /// </summary>
        public string FlightCode { get; }

        /// <summary>
        /// Gets the airline code associated with this flight (e.g. "JST").
        /// </summary>
        public Airline Airline { get; }

        /// <summary>
        /// Gets the unique identifier for the plane operating this flight.
        /// </summary>
        public string PlaneId { get; }

        /// <summary>
        /// Gets or sets the scheduled date and time for the flight in the format HH:mm dd/MM/yyyy.
        /// </summary>
        public DateTime Scheduled { get; private set; }

        private readonly Dictionary<string, string> _bookings = new();

        /// <summary>
        /// Initialises a new instance of the Flight class.
        /// </summary>
        /// <param name="flightCode">The unique flight code (e.g. "150").</param>
        /// <param name="airline">The airline code associated with the airline operating the flight (e.g. "JST")</param>
        /// <param name="planeId">The plane identifier.</param>
        /// <param name="scheduled">The scheduled flight time in the format HH:mm dd/MM/yyyy.</param>
        protected Flight(string flightCode, Airline airline, string planeId, DateTime scheduled)
        { FlightCode = flightCode; Airline = airline; PlaneId = planeId; Scheduled = scheduled; }

        /// <summary>
        /// Determines whether a specified seat is currently occupied.
        /// </summary>
        /// <param name="seat">The seat code to check ("5A").</param>
        /// <returns>TRUE, if the seat is occupied, or FALSE, if not occupied.</returns>
        public bool IsSeatOccupied(string seat) => _bookings.ContainsKey(seat);

        /// <summary>
        /// Attempts to book a seat for a user on this flight.
        /// </summary>
        /// <param name="seat">The seat code ("5A").</param>
        /// <param name="userEmail">The email of the user requesting to book the seat.</param>
        /// <returns>TRUE, if the booking was successful, or FALSE, if unsuccessful.</returns>
        public bool TryBookSeat(string seat, string userEmail) => !IsSeatOccupied(seat) && (_bookings[seat] = userEmail) != null;

        /// <summary>
        /// Reallocates a user to a different seat.
        /// </summary>
        /// <param name="seat">The seat code to assign (e.g. "5A").</param>
        /// <param name="userEmail">The email of the user receiving the seat.</param>
        public void ForceAssignSeat(string seat, string userEmail) => _bookings[seat] = userEmail;

        /// <summary>
        /// Retrieves all current seat bookings for this flight.
        /// </summary>
        /// <returns>A collection of tuples representing seat codes and their associated email addresses.</returns>
        public IEnumerable<(string seat, string email)> GetAllBookings() => _bookings.Select(kv => (kv.Key, kv.Value));
        
        /// <summary>
        /// Delays the scheduled flight time by a specified duration.
        /// </summary>
        /// <param name="span">The time span by which to delay the flight.</param>
        public void DelayBy(TimeSpan span) { Scheduled = Scheduled.Add(span); }
    }
}