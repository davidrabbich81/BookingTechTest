using BookingSystem.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem
{
    /// <summary>
    /// Helper methods to keep dependency injection tidy
    /// </summary>
    public static class DependencyInjectionExtensionMethods
    {

        /// <summary>
        /// Injects services into the dependency service builder
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder InjectServices(this WebApplicationBuilder builder)
        {
            // add services here

            return builder;
        }

        /// <summary>
        /// Injects data repositories into the dependency service builder
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder InjectRepositories(this WebApplicationBuilder builder)
        {
            // add repositories here

            return builder;
        }

        /// <summary>
        /// Handles the injection of the db context and all related services into the service builder
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder InjectDBContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


            builder.Services.AddDbContext<BookingSystemDbContext>(options =>
                options.UseSqlServer(connectionString));

            return builder;
        }
    }
}
