using BookingSystem.Data.Domain;
using BookingSystem.Data.Repositories;
using BookingSystem.Data.Repositories.Interface;
using BookingSystem.Options;
using BookingSystem.Services;
using BookingSystem.Services.Interface;
using BookingSystem.UnitTests.Mocks.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioApi.Tests.Services
{
    public class IoCService
    {
        #region Fields

        private Dictionary<string, object> _serviceDictionary = new Dictionary<string, object>();

        #endregion


        #region Ctors


        #endregion

        #region Methods

        private void DefineServices()
        {
            // add the db context
            AddService(new InMemoryBookingSystemDBContext().GetDBContext());

            // add the encryption service
            AddService<IEncryptionService>(new EncryptionService(
                Options.Create<EncryptionOptions>(new EncryptionOptions() { 
                    EncryptionKey = "v3c26vvfht7ert83cd84d02cake000a" 
                })
            ));

            // add the user repo
            AddService<IUserRepository>(
                new UserRepository(
                    GetService<BookingSystemDbContext>(), 
                    GetService<IEncryptionService>())
            );

            // add the booking repo
            AddService<IBookingRepository>(
                new BookingRepository(GetService<BookingSystemDbContext>())
                );

        }
        public T GetService<T>()
        {

            string serviceName = typeof(T).Name;
            if (_serviceDictionary.ContainsKey(serviceName))
                return (T)_serviceDictionary[serviceName];

            throw new ArgumentException($"The requested service of type: {nameof(T)} was not found");
        }

        private void AddService<TInterface>(TInterface instanceOfService) where TInterface : class
        {
            _serviceDictionary.Add(typeof(TInterface).Name, instanceOfService);
        }

        #endregion

        private static readonly IoCService _controller = new IoCService();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static IoCService()
        {
        }

        private IoCService()
        {
            DefineServices();
        }

        public static IoCService Controller
        {
            get
            {
                return _controller;
            }
        }

    }
}
