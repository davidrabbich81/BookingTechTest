namespace BookingSystem.Services.Interface
{
    public interface IEncryptionService
    {
        string DecryptText(string cipherText);
        string EncryptText(string content);
    }
}