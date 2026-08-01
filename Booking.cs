using System;

namespace BrisbaneAirportApp
{
    /// <summary>
    /// Represents the flight booking made by a user, including their unique user ID, 
    /// the associated flight or plane ID, and the seat they have been assigned.
    /// RESPONSIBLITY: Stores essential booking details to link a specific user 
    /// with a specific flight and seat within the Brisbane Airport App system.
    /// </summary>
    /// <param name="UserId">The unique identifer of the user who made the booking.</param>
    /// <param name="FlightPlaneId">The identifier of the flight or plane associated with the booking.</param>
    /// <param name="Seat">The seat assigned to the user for this booking.</param>
    /// <returns>A record containing the specified booking details.</returns>
    public record Booking
    (
        /// <summary>
        /// Gets the unique identifier of the user who made the booking.
        /// </summary>
        Guid UserId,

        /// <summary>
        /// Gets the identifier of the flight or plane associated with this booking.
        /// </summary>
        string FlightPlaneId,

        /// <summary>
        /// Gets the seat assigned to the user for this booking.
        /// </summary>
        string Seat
    );
}