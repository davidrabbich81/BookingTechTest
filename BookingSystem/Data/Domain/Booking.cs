using BookingSystem.Data.PostPutModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Data.Domain
{
    public class Booking
    {
        /// <summary>
        /// The unique ID of the booking
        /// </summary>
        public Guid BookingId { get; set; } = Guid.NewGuid();

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

    /// <summary>
    /// How flexible the booking can be around the chosen date
    /// </summary>
    public enum BookingFlexibility : byte
    {
        [Description("Exact date only")]
        ZeroDays = 0,
        [Description("+/- 1 Day")]
        OneDay = 1,
        [Description("+/- 2 Days")]
        TwoDays = 2,
        [Description("+/- 3 Days")]
        ThreeDays = 3
    }

    /// <summary>
    /// The size of vehicle needed (No description as names match perfectly
    /// </summary>
    public enum VehicleSize : byte
    {
        Small = 0,
        Medium = 1,
        Large = 2,
        Van = 3
    }

    public static class BookingExtensions
    {
        /// <summary>
        /// Creates a post put model to present to the UI
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public static PostPutBooking ToPostPutModel(this Booking booking)
        {
            PostPutBooking result = new PostPutBooking()
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
