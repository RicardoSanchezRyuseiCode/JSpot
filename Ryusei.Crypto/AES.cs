using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.Crypto
{
    /// <summary>
    /// Name: AES
    /// Description: Utility to encrypt and decrypt values
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     16/06/2018: Creation
    /// </summary>
    public class AES
    {
        private static byte[] GetEncryptionKey()
        {
            byte[] encryptionKeyBytes = null;
            using (var sha = new SHA256Managed())
            {
                encryptionKeyBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["SECURITY::ENCRYPTIONKEY"]));
            }
            return encryptionKeyBytes;
        }
        /// <summary>
        /// Name: Encrypt
        /// Description: Method to encrypt a string value
        /// </summary>
        /// <param name="value">Value</param>
        public static string Encrypt(string value)
        {
            var buffer = Encoding.UTF8.GetBytes(value);
            using (var inputStream = new MemoryStream(buffer, false))
            using (var outputStream = new MemoryStream())
            using (var aes = new AesManaged { Key = GetEncryptionKey() })
            {
                var iv = aes.IV;
                outputStream.Write(iv, 0, iv.Length);
                outputStream.Flush();
                var encryptor = aes.CreateEncryptor(GetEncryptionKey(), iv);
                using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                {
                    inputStream.CopyTo(cryptoStream);
                }
                return Convert.ToBase64String(outputStream.ToArray());
            }
        }
        /// <summary>
        /// Name: Decrypt
        /// Description: Method to decrypt a string value
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public static string Decrypt(string value)
        {
            // Read value from base 64
            var buffer = Convert.FromBase64String(value);
            using (var inputStream = new MemoryStream(buffer, false))
            using (var outputStream = new MemoryStream())
            using (var aes = new AesManaged { Key = GetEncryptionKey() })
            {
                var iv = new byte[16];
                var bytesRead = inputStream.Read(iv, 0, 16);
                if (bytesRead < 16)
                {
                    throw new CryptographicException("IV is missing or invalid.");
                }
                // Descrypt the value
                var decryptor = aes.CreateDecryptor(GetEncryptionKey(), iv);
                using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputStream);
                }
                var decryptedValue = Encoding.UTF8.GetString(outputStream.ToArray());
                return decryptedValue;
            }
        }
    }
}
