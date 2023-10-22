using BookingSystem.Data.Repositories;
using BookingSystem.Data.Repositories.Interface;
using BookingSystem.Services.Interface;
using PortfolioApi.Tests.Services;

namespace BookingSystem.UnitTests.Tests.Encryption
{
    public class UserRepositoryTests
    {
        private IUserRepository userRepository;

        [SetUp]
        public void Setup()
        {
            userRepository = IoCService.Controller.GetService<IUserRepository>();
        }

        [TestCase("admin", "admin", false)]
        [TestCase("testuser@djvaleting.com", "UP8UHoq&qa!$r^V2", true)]
        public async Task When_UserRepositoryAttemptLoginCalled_Then_LoginResultAsExpected(
            string emailAddress, string password, bool loginSuccessful)
        {
            var loginResult = await userRepository.AttemptLoginAsync(emailAddress, password);

            Assert.That(loginResult.Success, Is.EqualTo(loginSuccessful), "The login results did not match");
        }

    }
}