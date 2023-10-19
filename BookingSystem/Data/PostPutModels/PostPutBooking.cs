using BookingSystem.Data.Domain;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Data.PostPutModels
{
    public class PostPutBooking
    {
        /// <summary>
        /// The date the booking was created
        /// </summary>
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The full name of the person making the booking request
        /// </summary>
        [MaxLength(255)]
        public string? Name { get; set; }

        /// <summary>
        /// The Email address of the person making the booking request
        /// </summary>
        [MaxLength(500)]
        public string? EmailAddress { get; set; }

        /// <summary>
        /// The contact number of the person making the booking request
        /// </summary>
        [MaxLength(20)]
        public string? ContactNumber { get; set; }

        /// <summary>
        /// The date the booking was requested for
        /// </summary>
        public DateTime BookingDate { get; set; }

        /// <summary>
        /// How flexible the user is on their booking date
        /// </summary>
        public BookingFlexibility Flexibility { get; set; }

        /// <summary>
        /// The size of the vehicle needed for the booking
        /// </summary>
        public VehicleSize VehicleSize { get; set; }

        /// <summary>
        /// Has the booking been confirmed
        /// </summary>
        public bool Confirmed { get; set; }

        /// <summary>
        /// Is the booking deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// When was the booking confirmed
        /// </summary>
        public DateTime? DateConfirmed { get; set; }

        /// <summary>
        /// The Id of the user that confirmed the booking
        /// </summary>
        public Guid? ConfirmedByUserId { get; set; }

    }

    public static class PostPutBookingExtensions
    {
        /// <summary>
        /// Creates a domain model to save to the database
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public static Booking ToEFModel(this PostPutBooking booking)
        {
            Booking result = new Booking()
            {
                BookingDate = booking.BookingDate,
                Confirmed = booking.Confirmed,
                Deleted = booking.Deleted,
                ConfirmedByUserId = booking.ConfirmedByUserId,
                ContactNumber = booking.ContactNumber,
                DateConfirmed = booking.DateConfirmed,
                DateCreated = booking.DateCreated,
                EmailAddress = booking.EmailAddress,
                Flexibility = booking.Flexibility,
                VehicleSize = booking.VehicleSize,
                Name = booking.Name,
            };

            return result;
        }
    }
}
