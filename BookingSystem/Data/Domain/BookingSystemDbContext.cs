using Microsoft.EntityFrameworkCore;
using System;

namespace BookingSystem.Data.Domain
{
    public class BookingSystemDbContext : DbContext
    {

        /// <summary>
        /// The dataset for the bookings records
        /// </summary>
        public DbSet<Booking> Bookings { get; set; }


        public BookingSystemDbContext()
        {
        }

        public BookingSystemDbContext(DbContextOptions<BookingSystemDbContext> options)
            : base(options)
        {
        }

    }
}
