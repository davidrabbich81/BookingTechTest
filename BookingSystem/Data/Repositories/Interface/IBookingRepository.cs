using BookingSystem.Data.Domain;
using BookingSystem.Data.PostPutModels;

namespace BookingSystem.Data.Repositories.Interface
{
    public interface IBookingRepository
    {
        Task<bool> ConfirmBooking(Guid bookingId, bool confirmed);
        Task<bool> DeleteBooking(Guid bookingId);
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<Booking> GetBookingById(Guid bookingId);
        Task<IEnumerable<Booking>> GetAllConfirmedBookings();
        Task<Booking> CreateNewBooking(PostPutBooking booking);
        Task<Booking> UpdateBooking(Guid bookingId, PostPutBooking booking);

    }
}