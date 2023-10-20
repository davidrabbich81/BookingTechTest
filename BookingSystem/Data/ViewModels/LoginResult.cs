namespace BookingSystem.Data.ViewModels
{
    /// <summary>
    /// Holds the result of a login attempt
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// Indicates if the login was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The User Id of the logged in user
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// The name of the user logged in
        /// </summary>
        public string Name { get; set; }

        public LoginResult(bool success, Guid? id = null)
        {
            this.Success = success;
            this.UserId = id;
        }
    }
}
