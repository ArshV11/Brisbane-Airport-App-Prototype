using System;

namespace BrisbaneAirportApp
{
    /// <summary>
    /// Represents a registered traveller with a frequent flyer membership.
    /// RESPONSIBILITY: Extends the Traveller class to manage frequent flyer identification and points tracking.
    /// </summary>
    public class FrequentFlyer : Traveller
    {
        /// <summary>
        /// Gets the unique frequent flyer membership number.
        /// </summary>
        public int Ffnumber { get; }

        /// <summary>
        /// Gets the total number of accumulated frequent flyer points.
        /// </summary>
        public int Points { get; private set; }

        /// <summary>
        /// Initialises a new instance of the FrequentFlyer class.
        /// </summary>
        /// <param name="name">The frequent flyer's name.</param>
        /// <param name="age">The frequent flyer's age.</param>
        /// <param name="mobile">The frequent flyer's mobile phone number.</param>
        /// <param name="email">The frequent flyer's email address.</param>
        /// <param name="password">The frequent flyer's password.</param>
        /// <param name="ffnumber">The frequent flyer membership number associated with the frequent flyer.</param>
        /// <param name="points">The initial number of frequent flyer points given to the frequent flyer.</param>
        public FrequentFlyer(string name, int age, string mobile, string email, string password, int ffnumber, int points)
            : base(name, age, mobile, email, password) { Ffnumber = ffnumber; Points = points; }

        /// <summary>
        /// Adds frequent flyer points up to a maximum cap of one million.
        /// </summary>
        /// <param name="p">Number of points to be added.</param>
        public void AddPoints(int p) => Points = Math.Min(1000000, Points + p);

        /// <summary>
        /// Displays frequent flyer full profile information, including frequent flyer membership number
        /// and frequent flyer points, in a formatted string.
        /// </summary>
        /// <returns>The frequent flyer's full profile information as a formatted string</returns>
        public override string DisplayInfo() => base.DisplayInfo() + $"\nFrequent flyer number: {Ffnumber}\nFrequent flyer points: {Points.ToString("N0")}\n"; //originally just Points
        
        /// <summary>
        /// Displays the frequent flyer's current frequent flyer points balance.
        /// </summary>
        /// <returns>The frequent flyer's current number of frequent flyer points.</returns>
        public string DisplayFrequentFlyerPoints() => $"Your current points are: {Points.ToString("N0")}.";
    }
}