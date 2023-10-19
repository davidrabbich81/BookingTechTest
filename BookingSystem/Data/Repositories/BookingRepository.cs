using BookingSystem.Data.Domain;
using BookingSystem.Data.PostPutModels;
using BookingSystem.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Data.Repositories
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        private readonly BookingSystemDbContext dbContext;

        public BookingRepository(BookingSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Gets all none deleted bookings in the system
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Booking>> GetAllBookings()
            => await dbContext.Bookings
                    .Where(x => !x.Deleted)
                    .ToListAsync();

        /// <summary>
        /// Gets all confirmed bookings in the system
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Booking>> GetAllConfirmedBookings()
            => await dbContext.Bookings
                    .Where(x => !x.Deleted && x.Confirmed)
                    .ToListAsync();

        /// <summary>
        /// Marks a booking as deleted in the platform
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> DeleteBooking(Guid bookingId)
        {
            var booking = await dbContext.Bookings
                .FirstOrDefaultAsync(x => x.BookingId == bookingId);
            if (booking == null) throw new ArgumentException("A booking could not be found with the id supplied");

            booking.Deleted = true;

            await dbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Confirm 
        /// </summary>
        /// <param name="bookingId">The Id of the booking to change</param>
        /// <param name="confirmed">Indicates whether to confirm the booking or not</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> ConfirmBooking(Guid bookingId, bool confirmed)
        {
            var booking = await dbContext.Bookings
                .FirstOrDefaultAsync(x => x.BookingId == bookingId);
            if (booking == null) throw new ArgumentException("A booking could not be found with the id supplied");

            booking.Confirmed = confirmed;
            booking.DateConfirmed = !confirmed ? null : DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Creates a new booking in the system
        /// </summary>
        /// <param name="booking">the booking info to add</param>
        /// <returns></returns>
        public async Task<Booking> CreateNewBooking(PostPutBooking booking)
        {
            var newBooking = booking.ToEFModel();

            // ensure any new booking is added as unconfirmed
            newBooking.Confirmed = false;
            newBooking.ConfirmedByUserId = null;
            newBooking.DateConfirmed = null;

            dbContext.Add(newBooking);

            await dbContext.SaveChangesAsync();

            return newBooking;
        }

        /// <summary>
        /// Updates a booking in the system
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="booking"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Booking> UpdateBooking(Guid bookingId, PostPutBooking booking)
        {
            var existingBooking = await dbContext.Bookings
                .FirstOrDefaultAsync(x => x.BookingId == bookingId);
            if (existingBooking == null) throw new ArgumentException("A booking could not be found with the id supplied");

            existingBooking.ContactNumber = booking.ContactNumber;
            existingBooking.EmailAddress = booking.EmailAddress;
            existingBooking.Name = booking.Name;
            existingBooking.Flexibility = booking.Flexibility;
            existingBooking.VehicleSize = booking.VehicleSize;

            await dbContext.SaveChangesAsync();

            return existingBooking;
        }
    }
}
