using System;

namespace BrisbaneAirportApp
{
    /// <summary>
    /// Represents the base class for all user types in the Brisbane Airport system.
    /// RESPONSIBILITY: Encapsulates user identity, authentication and shared personal information.
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// Gets the unique identifier for the user.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the user's full name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the user's age.
        /// </summary>
        public int Age { get; protected set; }

        /// <summary>
        /// Gets or sets the user's mobile phone number.
        /// </summary>
        public string Mobile { get; protected set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; protected set; }

        private string _passwordHash;

        /// <summary>
        /// Initialises a new instance of the User class.
        /// </summary>
        /// <param name="name">The user's full name.</param>
        /// <param name="age">The user's age.</param>
        /// <param name="mobile">The user's mobile phone number.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        protected User(string name, int age, string mobile, string email, string password)
        {
            Name = name; Age = age; Mobile = mobile; Email = email;
            _passwordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Verifies whether a provided password matches the stored password
        /// </summary>
        /// <param name="password">The password provided by the user.</param>
        /// <returns>TRUE, if the password matches, or FALSE, if it does not match.</returns>
        public bool Authenticate(string password) => _passwordHash == Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));

        /// <summary>
        /// Attempts to change the user's password after verifying the old one.
        /// </summary>
        /// <param name="old">The user's old password.</param>
        /// <param name="new">The user's new password.</param>
        /// <returns>
        /// TRUE, if the old password is verified and the new one is successfully changed, or
        /// FALSE, if either old password is not verified or new password is not successfully changed.
        /// </returns>
        public bool ChangePassword(string old, string @new) => Authenticate(old) && (_passwordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(@new))) != null;
        
        /// <summary>
        /// Displays the user's profile information as a formatted string.
        /// </summary>
        /// <returns>The user's profile information as a formatted string.</returns>
        public virtual string DisplayInfo() => $"Name: {Name}\nAge: {Age}\nMobile phone number: {Mobile}\nEmail: {Email}";
    }
}