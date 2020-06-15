using System;
namespace Dech.Hal.Banking.Contracts.Interfaces
{
    public interface IServiceDataProtection
    {
        //string Decrypt(string data);
        //string Encrypt(string data);

        //string EncryptGcm(string data);
        //string DecryptGcm(string data);

        string CalculateHash(string input);
        bool CheckMatch(string hash, string input);

        string Encrypt<T>(T obj);
        string Encryp(string plaintext);
        bool TryDecrypt<T>(string encryptedText, out T obj);
        bool TryDecrypt(string encryptedText, out string decryptedText);


    }
}
