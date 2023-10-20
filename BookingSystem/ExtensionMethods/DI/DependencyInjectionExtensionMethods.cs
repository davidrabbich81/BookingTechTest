using BookingSystem.Data.Domain;
using BookingSystem.Data.Repositories;
using BookingSystem.Data.Repositories.Interface;
using BookingSystem.Options;
using BookingSystem.Services;
using BookingSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            builder.Services.AddTransient<IEncryptionService, EncryptionService>();

            return builder;
        }

        public static WebApplicationBuilder InjectOptions(this WebApplicationBuilder builder)
        {
            // add options here

            builder.Services.Configure<EncryptionOptions>(options => 
                builder.Configuration.GetSection(nameof(EncryptionOptions)).Bind(options)
            );


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
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IBookingRepository, BookingRepository>();

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


            builder.Services.AddDbContextPool<BookingSystemDbContext>(options =>
                options.UseSqlServer(connectionString));

            return builder;
        }
    }
}
