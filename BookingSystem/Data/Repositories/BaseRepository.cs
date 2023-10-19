using System.Security.Claims;

namespace BookingSystem.Data.Repositories
{
    public class BaseRepository
    {
        /// <summary>
        /// The Identity of the current user
        /// </summary>
        public ClaimsIdentity Identity { get; set; }

        /// <summary>
        /// Sets the identity of a user in the repository
        /// </summary>
        /// <param name="identity"></param>
        public void SetIdentity(ClaimsIdentity identity)
        {

        }
    }
}
