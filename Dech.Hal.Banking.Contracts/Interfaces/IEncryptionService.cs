using System;
namespace Dech.Hal.Banking.Contracts.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string input);
        string Encrypt<T>(T instance);
        string Decrypt(string cipherText);
        bool TryDecrypt(string cipherText, out string decryptedText);
        bool TryDecrypt<T>(string cipherText, out T instance);
    }
}
