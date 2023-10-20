using BookingSystem.Data.Domain;
using BookingSystem.Data.Repositories.Interface;
using BookingSystem.Data.ViewModels;
using BookingSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookingSystemDbContext dbContext;
        private readonly IEncryptionService encryptionService;

        public UserRepository(BookingSystemDbContext dbContext, IEncryptionService encryptionService)
        {
            this.dbContext = dbContext;
            this.encryptionService = encryptionService;
        }

        /// <summary>
        /// Attempts a login against a known user in the database
        /// </summary>
        /// <param name="emailAddress">The email address of the user</param>
        /// <param name="password">The password that will be encrypted and tested against the database</param>
        /// <returns></returns>
        public async Task<LoginResult> AttemptLoginAsync(string emailAddress, string password)
        {
            var encryptedPassword = encryptionService.EncryptText(password);
            var user = await dbContext.Users.FirstOrDefaultAsync(
                x => x.Emailaddress == emailAddress &&
                x.Password == encryptedPassword &&
                !x.Deleted
            );

            if (user == null)
                return new LoginResult(success: false);

            await MarkUserAsUpdated(user.UserId, DateTime.UtcNow);
            return new LoginResult(success: true, id: user.UserId) { Name = user.Emailaddress };
        }

        /// <summary>
        /// Marks the users last login date as the provided date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateOfLogin"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task MarkUserAsUpdated(Guid userId, DateTime dateOfLogin)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) throw new ArgumentException("There was no user found with that Id");

            user.DateLastLogin = dateOfLogin;

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Marks a user as deleted in the system
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deleted"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task MarkUserAsDeleted(Guid userId, bool deleted)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) throw new ArgumentException("There was no user found with that Id");

            user.Deleted = deleted;

            await dbContext.SaveChangesAsync();
        }
    }
}
