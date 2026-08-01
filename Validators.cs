using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace BrisbaneAirportApp
{
    /// <summary>
    /// Provides static validation methods for user and flight data.
    /// RESPONSIBILITY: Ensures all input fields conform to business and formatting rules for registration, booking and flight management.
    /// </summary>
    public static class Validators
    {
        private static readonly Regex _password = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
        private static readonly Regex _seat = new(@"^(?:[1-9]|10)[A-D]$");
        private static readonly Regex _dateTime = new(@"^([01]\d|2[0-3]):([0-5]\d)\s(0[1-9]|[12]\d|3[01])/(0[1-9]|1[0-2])/(\d{4})$");
        private static readonly Regex _seatColumn = new(@"^[ABCD]$");

        /// <summary>
        /// Validates that a name contains only letters, spaces, hyphens or apostrophes.
        /// </summary>
        /// <param name="n">The name as a string.</param>
        /// <returns>TRUE, if name is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidName(string n) => !string.IsNullOrWhiteSpace(n) && n.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '\'');

        /// <summary>
        /// Validates that the age is a number less thabn three digits long.
        /// </summary>
        /// <param name="a">The age as a string.</param>
        /// <returns>TRUE, if age is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidAge(string a) => a.Length < 3 && a.All(c => char.IsNumber(c));

        /// <summary>
        /// Validates that a mobile number starts with "0" and contains exactly ten digits. 
        /// </summary>
        /// <param name="m">The mobile number as a string.</param>
        /// <returns>TRUE, if mobile number is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidMobile(string m) => m.Length == 10 && m.All(c => char.IsNumber(c)) && m.StartsWith("0");

        /// <summary>
        /// Validates that the email address contains one "@" symbol and non-empty parts on either side of the symbol.
        /// </summary>
        /// <param name="e">The email address as a string.</param>
        /// <returns>TRUE, if email address is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidEmail(string e) => !string.IsNullOrWhiteSpace(e) && e.Split('@') is var p && p.Length == 2 && p[0].Length > 0 && p[1].Length > 0;

        /// <summary>
        /// Validates that a password meets minimum security requirements.
        /// </summary>
        /// <param name="p">The password as a string.</param>
        /// <returns>TRUE, if password is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidPassword(string p) => !string.IsNullOrEmpty(p) && _password.IsMatch(p);

        /// <summary>
        /// Validates that a flight ID is between 100 and 900.
        /// </summary>
        /// <param name="fid">The flight ID as in integer.</param>
        /// <returns>TRUE, if flight ID is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidFlightId(int fid) => fid is >= 100 and <= 900;

        /// <summary>
        /// Validates that a plane ID is between 0 and 9.
        /// </summary>
        /// <param name="pid">The plane ID as an integer.</param>
        /// <returns>TRUE, if the plane ID is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidPlaneId(int pid) => pid is >= 0 and <= 9;

        /// <summary>
        /// Validates that the seat code must contain a number followed by a capital letter.
        /// </summary>
        /// <param name="s">The seat code as string.</param>
        /// <returns>TRUE, if the seat code is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidSeat(string s) => !string.IsNullOrEmpty(s) && _seat.IsMatch(s);

        /// <summary>
        /// Validates that the individual seat columns are either "A", "B", "C" or "D".
        /// </summary>
        /// <param name="sc">The seat column as a string.</param>
        /// <returns>TRUE, if seat column is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidSeatColumn(string sc) => !string.IsNullOrEmpty(sc) && _seatColumn.IsMatch(sc);

        /// <summary>
        /// Validates that the frequent flyer number is between 100,000 and 999,999.
        /// </summary>
        /// <param name="n">The frequent flyer number as in integer.</param>
        /// <returns>TRUE, if frequent flyer number is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidFFNumber(int n) => n is >= 100000 and <= 999999;

        /// <summary>
        /// Validates that the number of frequent flyer points are between 0 and 1,000,000.
        /// </summary>
        /// <param name="n">The number of frequent flyer points as an integer.</param>
        /// <returns>TRUE, if the number of frequent flyer points are valid, or FALSE, if it is invalid.</returns>
        public static bool ValidFFPoints(int n) => n is >= 0 and <= 1000000;

        /// <summary>
        /// Validates that the staff ID number is between 1,000 and 9,000.
        /// </summary>
        /// <param name="n">The staff ID number as an integer.</param>
        /// <returns>TRUE, if the staff ID number is valid, or FALSE, if it is invalid.</returns>
        public static bool ValidStaffId(int n) => n is >= 1000 and <= 9000;
        
        /// <summary>
        /// Validates that the date and time follow the format HH:mm dd/MM/yyyy.
        /// </summary>
        /// <param name="dt">The date and time as a string.</param>
        /// <returns>TRUE, if the date and time is in a valid format, or FALSE, if it is invalid.</returns>
        public static bool ValidDateTime(string dt) => !string.IsNullOrEmpty(dt) && _dateTime.IsMatch(dt);
    }
}