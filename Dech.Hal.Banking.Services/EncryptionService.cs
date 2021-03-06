﻿using System;
using System.IO;
using System.Security.Cryptography;
using Dech.Hal.Banking.Contracts;
using Dech.Hal.Banking.Contracts.Interfaces;
using Newtonsoft.Json;

namespace Dech.Hal.Banking.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly KeyInfo _keyInfo;

        public EncryptionService(KeyInfo keyInfo = null)
        {
            _keyInfo = keyInfo;
        }

        public string Encrypt(string input)
        {
            var enc = EncryptStringToBytes_Aes(input, _keyInfo.Key, _keyInfo.Iv);
            return Convert.ToBase64String(enc);
        }


        public string Encrypt<T>(T instance)
        {
            var json = JsonConvert.SerializeObject(instance);

            return Encrypt(json);
        }


        public string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            return DecryptStringFromBytes_Aes(cipherBytes, _keyInfo.Key, _keyInfo.Iv);
        }

        public bool TryDecrypt<T>(string cipherText, out T instance)
        {
            if (TryDecrypt(cipherText, out var json))
            {
                instance = JsonConvert.DeserializeObject<T>(json);

                return true;
            }

            instance = default(T);

            return false;
        }

        public bool TryDecrypt(string cipherText, out string decryptedText)
        {
            try
            {
                decryptedText = Decrypt(cipherText);

                return true;
            }
            catch (CryptographicException)
            {
                decryptedText = null;

                return false;
            }
        }

        #region


        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));
            byte[] encrypted;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            string plaintext;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }

        #endregion
    }
}
