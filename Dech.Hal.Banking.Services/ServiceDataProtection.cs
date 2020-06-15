using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Dech.Hal.Banking.Contracts;
using Dech.Hal.Banking.Contracts.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace Dech.Hal.Banking.Services
{
    public class ServiceDataProtection : IServiceDataProtection
    {
        private readonly IDataProtector _protector;
        private readonly UniqueCode _uniqueCode;

        private const int KEY_BYTES = 16;
        private const int NONCE_BYTES = 12;


        public ServiceDataProtection(IDataProtectionProvider dataProtectionProvider, UniqueCode uniqueCode)
        {
            _protector = dataProtectionProvider.CreateProtector(uniqueCode.SecrectKey);
            _uniqueCode = uniqueCode;
        }

        //public ServiceDataProtection( UniqueCode uniqueCode)
        //{
        //    //protector = dataProtectionProvider.CreateProtector(uniqueCode.SecrectKey);
        //    _uniqueCode = uniqueCode;
        //}

        //public string EncryptGcm(string data)
        //{
        //    byte[] value = System.Text.Encoding.Unicode.GetBytes(data);
        //    byte[] key = Encoding.UTF8.GetBytes(_uniqueCode.Key);

        //   // var cypher = new MayMeow.Cryptography.GCM();

        //    var encrypted = MayMeow.Cryptography.GCM.Encrypt(value, key);
        //    return Convert.ToBase64String(encrypted);
        //}

        //public string DecryptGcm(string data)
        //{
        //    byte[] encrypted = Convert.FromBase64String(data);
        //    byte[] key = Encoding.UTF8.GetBytes(_uniqueCode.Key);

        //    var decrypted = MayMeow.Cryptography.GCM.Decrypt(encrypted, key);
        //    return Encoding.Unicode.GetString(decrypted);
        //}


        //public byte[] Encrypt(byte[] toEncrypt, byte[] key, byte[] associatedData = null)
        //{
        //    byte[] tag = new byte[KEY_BYTES];
        //    byte[] nonce = new byte[NONCE_BYTES];
        //    byte[] cipherText = new byte[toEncrypt.Length];

        //    using (var cipher = new AesGcm(key))
        //    {
        //        cipher.Encrypt(nonce, toEncrypt, cipherText, tag, associatedData);

        //        return Concat(tag, Concat(nonce, cipherText));
        //    }
        //}

        //public byte[] Decrypt(byte[] cipherText, byte[] key, byte[] associatedData = null)
        //{
        //    byte[] tag = SubArray(cipherText, 0, KEY_BYTES);
        //    byte[] nonce = SubArray(cipherText, KEY_BYTES, NONCE_BYTES);

        //    byte[] toDecrypt = SubArray(cipherText, KEY_BYTES + NONCE_BYTES, cipherText.Length - tag.Length - nonce.Length);
        //    byte[] decryptedData = new byte[toDecrypt.Length];

        //    using (var cipher = new AesGcm(key))
        //    {
        //        cipher.Decrypt(nonce, toDecrypt, tag, decryptedData, associatedData);

        //        return decryptedData;
        //    }
        //}




        //public string Encrypt_Old(string data)
        //{

        //    Aes cipehr = CreateAesCipher();
        //    byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(data);

        //    ICryptoTransform encryptor = cipehr.CreateEncryptor();

        //    var encryptedBytes = encryptor.TransformFinalBlock(encodedBytes, 0, encodedBytes.Length);

        //    return Convert.ToBase64String(encryptedBytes);

        //}


        //public string Decrypt_old(string data)
        //{
        //    Aes cipehr = CreateAesCipher();

        //    byte[] encryptedBytes = Convert.FromBase64String(data);
        //    ICryptoTransform encryptor = cipehr.CreateEncryptor();

        //    var decryptedBytes = encryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        //    return System.Text.Encoding.Unicode.GetString(decryptedBytes);
        //}





       // public string EncryptAes(string data)
       // {
       //     string encrypted = string.Empty;
       //     using (Aes aesCypher = Aes.Create())
       //     {
       //         aesCypher.Padding = PaddingMode.ISO10126;
       //         aesCypher.Key =  Encoding.UTF8.GetBytes(_uniqueCode.Key);

       //         byte[] encryptedBytes = EncryptAes(data, aesCypher.Key, aesCypher.IV);
       //         encrypted = Convert.ToBase64String(encryptedBytes);
       //     }

       //     return encrypted;

       //     return _protector.Protect(data);



       //     byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(data);
       //     //string value = Convert.ToBase64String(encodedBytes);
       //     //return protector.Protect(data);

       //     var dataBytes = _protector.Protect(encodedBytes);

       //     return Convert.ToBase64String(dataBytes);



       // }


       // private Aes CreateAesCipher()
       // {
       //     Aes cipher = Aes.Create();
       //     cipher.Padding = PaddingMode.ISO10126;
       //     cipher.Key = Encoding.UTF8.GetBytes(_uniqueCode.Key);

       //     return cipher;
       // }



       // public string DecryptAES(string data)
       // {
       //     string decrypted = string.Empty;
       //     using (Aes aesCypher = Aes.Create())
       //     {
       //         aesCypher.Key = Encoding.UTF8.GetBytes(_uniqueCode.Key);
       //         byte[] encryptedBytes = Convert.FromBase64String(data);
       //         decrypted = DecrypAes(encryptedBytes, aesCypher.Key, aesCypher.IV);
       //     }

       //     return decrypted;



       //     return _protector.Unprotect(data);


       //     byte[] dataBytes = Convert.FromBase64String(data);
       //     byte[] value = _protector.Unprotect(dataBytes);
       //     return System.Text.Encoding.Unicode.GetString(value);

       //     //var value=  protector.Unprotect(data);
       //     //byte[] decodedBytes = Convert.FromBase64String(value);
       //     //return System.Text.Encoding.Unicode.GetString(decodedBytes);

       // }


   

       //private static byte[] EncryptAes(string cipherData, byte[] Key, byte[] IV)
       // {
       //     if (cipherData == null || cipherData.Length <= 0)
       //         throw new ArgumentNullException("plainText");
       //     if (Key == null || Key.Length <= 0)
       //         throw new ArgumentNullException("Key");
       //     if (IV == null || IV.Length <= 0)
       //         throw new ArgumentNullException("IV");
       //     byte[] encrypted;

       //     using (Aes aesAlgorithm = Aes.Create())
       //     {
       //         aesAlgorithm.Key = Key;
       //         aesAlgorithm.IV = IV;

       //         ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);
       //         using (MemoryStream msEncrypt = new MemoryStream())
       //         {
       //             using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
       //             {
       //                 using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
       //                 {
       //                     swEncrypt.Write(cipherData);
       //                 }
       //                 encrypted = msEncrypt.ToArray();
       //             }
       //         }
       //     }
       //     return encrypted;
       // }

       // static string DecrypAes(byte[] cipherData, byte[] Key, byte[] IV)
       // {
       //     if (cipherData == null || cipherData.Length <= 0)
       //         throw new ArgumentNullException("cipherText");
       //     if (Key == null || Key.Length <= 0)
       //         throw new ArgumentNullException("Key");
       //     if (IV == null || IV.Length <= 0)
       //         throw new ArgumentNullException("IV");

       //     string sensitiveData = null;

       //     using (Aes aesAlgorithm = Aes.Create())
       //     {
       //         aesAlgorithm.Key = Key;
       //         aesAlgorithm.IV = IV;

       //         ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, aesAlgorithm.IV);

       //         using (MemoryStream msDecrypt = new MemoryStream(cipherData))
       //         {
       //             using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
       //             {
       //                 using (StreamReader srDecrypt = new StreamReader(csDecrypt))
       //                 {
       //                     sensitiveData = srDecrypt.ReadToEnd();
       //                 }
       //             }
       //         }
       //     }

       //     return sensitiveData;
       // }



       // private static byte[] EncryptData(string cipherData, byte[] Key, byte[] IV)
       // {
       //     if (cipherData == null || cipherData.Length <= 0)
       //         throw new ArgumentNullException("plainText");
       //     if (Key == null || Key.Length <= 0)
       //         throw new ArgumentNullException("Key");
       //     if (IV == null || IV.Length <= 0)
       //         throw new ArgumentNullException("IV");
       //     byte[] encrypted;

       //     using (Aes aesAlgorithm = Aes.Create())
       //     {
       //         aesAlgorithm.Key = Key;
       //         aesAlgorithm.IV = IV;

       //         ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);
       //         using (MemoryStream msEncrypt = new MemoryStream())
       //         {
       //             using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
       //             {
       //                 using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
       //                 {
       //                     swEncrypt.Write(cipherData);
       //                 }
       //                 encrypted = msEncrypt.ToArray();
       //             }
       //         }
       //     }
       //     return encrypted;
       // }




       // public static byte[] Concat(byte[] a, byte[] b)
       // {
       //     byte[] output = new byte[a.Length + b.Length];

       //     for (int i = 0; i < a.Length; i++)
       //     {
       //         output[i] = a[i];
       //     }

       //     for (int j = 0; j < b.Length; j++)
       //     {
       //         output[a.Length + j] = b[j];
       //     }

       //     return output;
       // }

       // public static byte[] SubArray(byte[] data, int start, int length)
       // {
       //     byte[] result = new byte[length];

       //     Array.Copy(data, start, result, 0, length);

       //     return result;
       // }



        //-------

        public class RandomGenerator
        {
            private const string AllowableCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";

            public static string GenerateString(int length)
            {
                var bytes = new byte[length];

                using (var random = RandomNumberGenerator.Create())
                {
                    random.GetBytes(bytes);
                }

                return new string(bytes.Select(x => AllowableCharacters[x % AllowableCharacters.Length]).ToArray());
            }
        }


        public string CalculateHash(string input)
        {
            var salt = GenerateSalt(16);

            var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

            return $"{ Convert.ToBase64String(salt) }:{ Convert.ToBase64String(bytes) }";
        }

        public bool CheckMatch(string hash, string input)
        {
            try
            {
                var parts = hash.Split(':');

                var salt = Convert.FromBase64String(parts[0]);

                var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

                return parts[1].Equals(Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
        }


        private static byte[] GenerateSalt(int length)
        {
            var salt = new byte[length];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return salt;
        }

        //--------

        public string Encrypt<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return Encryp(json);
        }

        public string Encryp(string plaintext)
        {
            return _protector.Protect(plaintext);
        }

        public bool TryDecrypt<T>(string encryptedText, out T obj)
        {
            if (TryDecrypt(encryptedText, out var json))
            {
                obj = JsonConvert.DeserializeObject<T>(json);

                return true;
            }

            obj = default(T);

            return false;
        }

        public bool TryDecrypt(string encryptedText, out string decryptedText)
        {
            try
            {
                decryptedText = _protector.Unprotect(encryptedText);

                return true;
            }
            catch (CryptographicException)
            {
                decryptedText = null;

                return false;
            }
        }


    }
}
