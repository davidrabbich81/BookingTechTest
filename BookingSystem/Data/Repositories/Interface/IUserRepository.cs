using BookingSystem.Data.ViewModels;

namespace BookingSystem.Data.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<LoginResult> AttemptLoginAsync(string emailAddress, string password);
        Task MarkUserAsDeleted(Guid userId, bool deleted);
        Task MarkUserAsUpdated(Guid userId, DateTime dateOfLogin);
    }
}