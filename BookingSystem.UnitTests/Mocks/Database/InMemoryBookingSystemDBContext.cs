using BookingSystem.Data.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.UnitTests.Mocks.Database
{
    internal class InMemoryBookingSystemDBContext
    {
        private SqliteConnection _connection;
        private DbContextOptions<BookingSystemDbContext> _options;

        public BookingSystemDbContext GetDBContext()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            _options = new DbContextOptionsBuilder<BookingSystemDbContext>()
                .UseSqlite(_connection)
                .Options;

            var context = new BookingSystemDbContext(_options);
            context.Database.EnsureCreated();
            GenerateMockData(context);
            return context;
        }

        private void GenerateMockData(BookingSystemDbContext context)
        {
            // generate users 
            GenerateUsers(context);

            //generate bookings
            GenerateBookings(context);

            context.SaveChanges();
        }

        private void GenerateBookings(BookingSystemDbContext context)
        {
            context.Bookings.Add(new Booking()
            {
                BookingId = Guid.NewGuid(),
                BookingDate = DateTime.Today.AddDays(2),
                DateCreated = DateTime.UtcNow,
                EmailAddress = "email@address.com",
                Name = "Booking contact",
                ContactNumber = "0000000"
            });
        }

        private static void GenerateUsers(BookingSystemDbContext context)
        {
            context.Users.Add(new User()
            {
                DateCreated = DateTime.UtcNow,
                Emailaddress = "testuser@djvaleting.com",
                Password = "c73ocOI3ulrl6CDd1CfKce9HUIjnI0KmsJl7Qcq/WGs=",
                UserId = Guid.NewGuid()
            });
        }


    }
}
