using BookingSystem.Services.Interface;
using PortfolioApi.Tests.Services;

namespace BookingSystem.UnitTests.Tests.Encryption
{
    public class EncryptionServiceTests
    {
        private IEncryptionService encryptionService;

        [SetUp]
        public void Setup()
        {
            encryptionService = IoCService.Controller.GetService<IEncryptionService>();
        }

        [Test]
        public void When_EncryptionServiceCallsEncrypt_Then_TextShouldBeEncrypted()
        {
            var text = "I am a message to encrypt";
            var encrytpedText = encryptionService.EncryptText(text);

            Assert.That(encrytpedText, Is.Not.Null.And.Not.Empty, "The encrypted text was empty");

            Console.WriteLine(encrytpedText);
        }

        [Test]
        public void Generate_SeedPassword_ForSeedUser()
        {
            var text = "UP8UHoq&qa!$r^V2";
            var encrytpedText = encryptionService.EncryptText(text);

            Assert.That(encrytpedText, Is.Not.Null.And.Not.Empty, "The encrypted text was empty");

            Console.WriteLine(encrytpedText);
        }
    }
}