using BookingSystem.Options;
using BookingSystem.Services.Interface;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace BookingSystem.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IOptions<EncryptionOptions> options;

        public EncryptionService(IOptions<EncryptionOptions> options)
        {
            this.options = options;
        }

        public string EncryptText(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return content;

            if (string.IsNullOrWhiteSpace(this.options.Value.EncryptionKey))
                throw new Exception("No encryption key present in UserSecrets");

            Aes AES = Aes.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(this.options.Value.EncryptionKey);
            byte[] aesKey = SHA256Managed.Create().ComputeHash(passwordBytes);
            byte[] aesIV = MD5.Create().ComputeHash(passwordBytes);
            AES.Key = aesKey;
            AES.IV = aesIV;
            AES.Mode = CipherMode.CBC;
            AES.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = AES.CreateEncryptor(AES.Key, AES.IV);
            byte[] encrypted;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(content);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(encrypted);
        }


        public string DecryptText(string cipherText)
        {
            if (string.IsNullOrWhiteSpace(cipherText) || !cipherText.IsBase64String())
                return cipherText;

            if (string.IsNullOrWhiteSpace(this.options?.Value?.EncryptionKey))
                return cipherText; // if encryption not setup correctly, return the original text

            string plainText;

            try
            {
                Aes AES = Aes.Create();
                byte[] passwordBytes = Encoding.UTF8.GetBytes(this.options.Value.EncryptionKey);
                byte[] aesKey = SHA256Managed.Create().ComputeHash(passwordBytes);
                byte[] aesIV = MD5.Create().ComputeHash(passwordBytes);
                AES.Key = aesKey;
                AES.IV = aesIV;
                AES.Mode = CipherMode.CBC;
                AES.Padding = PaddingMode.PKCS7;
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                ICryptoTransform decryptor = AES.CreateDecryptor(AES.Key, AES.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // generally due to the text to decrypt is actually not encrypted, so return the orginal text 
                return cipherText;
            }

            return plainText;
        }
    }
}
