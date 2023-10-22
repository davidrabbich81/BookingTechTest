using BookingSystem.Data.Repositories;
using BookingSystem.Data.Repositories.Interface;
using BookingSystem.Services.Interface;
using PortfolioApi.Tests.Services;

namespace BookingSystem.UnitTests.Tests.Encryption
{
    public class BookingRepositoryTests
    {
        private IBookingRepository bookingRepository;

        [SetUp]
        public void Setup()
        {
            bookingRepository = IoCService.Controller.GetService<IBookingRepository>();
        }

        [Test]
        public async Task When_BookingRepositoryGetBookingsIsCalled_BookingsAreReturned()
        {
            var result = await bookingRepository.GetAllBookings();

            Assert.That(result, Is.Not.Empty, "The bookings list is empty");
        }

    }
}