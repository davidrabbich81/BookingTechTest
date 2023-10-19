using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Data.Domain
{
    public class User
    {
        /// <summary>
        /// The Unique ID of the user
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The Email address of the user
        /// </summary>
        [MaxLength(500)]
        public string? Emailaddress { get; set; }

        /// <summary>
        /// The encrypted password of the user
        /// </summary>
        [MaxLength(255)]
        public string? Password { get; set; }

        /// <summary>
        /// The date the user was created in the system
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The last time the user logged in
        /// </summary>
        public DateTime? DateLastLogin { get; set; }

        /// <summary>
        /// Is the user deleted from the system and can't login
        /// </summary>
        public bool Deleted { get; set; }
    }
}
