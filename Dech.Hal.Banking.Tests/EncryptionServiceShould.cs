using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Dech.Hal.Banking.Contracts;
using Dech.Hal.Banking.Contracts.Interfaces;
using Dech.Hal.Banking.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Dech.Hal.Banking.Tests
{
    public class EncryptionServiceShould: UnitTestBase
    {
        public EncryptionServiceShould(ITestOutputHelper output): base(output)
        {
            serviceProvider = RegisterServices();
        }


        [Fact]
        public void EncryptSensitiveData()
        {
            string sensitiveData = "hash my key is part of the test";



            var sut = serviceProvider.GetRequiredService<IEncryptionService>();

            var result = sut.Encrypt(sensitiveData);

            Assert.NotNull(result);
            Output.WriteLine(result);
        }


        [Fact]
        public void DecryptSensitiveData()
        {

            string expected = "hash my key is part of the test";
            string encryptedData = "Ic+MImh7a9yeEjHsRVHhKIszjGs70nYBLz1JAdPJDQ4=";


            var sut = serviceProvider.GetRequiredService<IEncryptionService>();

            var result = sut.Decrypt(encryptedData);

            Assert.Equal(expected, result);
            Output.WriteLine(result);


        }



        [Fact]
        public void EncryptSensitiveObject()
        {

            var customerService = serviceProvider.GetRequiredService<ICustomerService>();
            List<Customer> customers = customerService.GetCustomers().ToList();

            var sut = serviceProvider.GetRequiredService<IEncryptionService>();

            var result = sut.Encrypt<List<Customer>>(customers);

            Assert.NotNull(result);
            Output.WriteLine(result);
        }


        [Fact]
        public void DecryptSensitiveObject()
        {

            KeyInfo keyInfo = new KeyInfo();

            string key = keyInfo.KeyString;
            Output.WriteLine($"Key: {key}");
            string iv = keyInfo.IVString;

            Output.WriteLine($"IV: {iv}");


            string encryptedData = "huVTfALX6uZdc2IgQjdnkoLjF5qmjbYZ7jv1ivM5i52kcTdhckoaJFafow46TJkx4GBp+ocRRo5kJC6s16qMlM98rp3/MV5EXFPdpOeZyMXt8NFFN7O9AAT0rLNdr0sRtRnB/4FZsMHovdW8d897JrTjZs3NQx5S2cWjY9OMt0CCtcZMam+FcYDFFFh6WVr5xAzcABg8p3pJNJzTwTj2DzKWOLrdW+Gup/YKRTTfwEz2/nsEkEITGlymoUb1EOmtdv6e98dSSnwjPc+lE2XgcS4iEtgCKEdfvm8w5m8WWYJEUg3rGUO9joao2cMB830pn6glUut3XklKzeENg/dA6hZuzPAjtKvD61GEmxrySTZUvJppbkp097t51ch+YcSOUlSLBi2bUDFSpsUVQLyK5bElxqS+GWcIjLISjTBVDo/5QFCMXwVfEZo5mZOXC1vkWlmvTcLWRWbf8WkitA9vl3qq7cR6KBZceIWlnHwCE25UBSuEpJqach/11IyycmrU26z8inGlgWpKUSqtHJYg6szd1S6Ml/Zl5Jtv8YUJ9cqpuJ5SoD77Z9tDfGIzTdAzY4ZZEsZNPNx1vkgqPNz/JnpW6MBaoMRBo7vZtJc3fy0iTZpKRP7eNsxLB1VKBfyVt6lrRzDOqoeYHlkIsOFutuEWKFgkdxXfGv5qKrU3Ewpsn54J1cJJQhkObuRqz7Ep+bpMFAV+Z8NhnYcj8R9flROygzITHdrnlzw7vwd8BXICxANrs+Ym/BnFIFxNuNPWVjhbmuJhvE4OGEe+e2txdbBMrPVCcmMnmpv9/aHUOwOXxICjud/rVrgCUM76QPJwnjjdvmuW+WuwNk9j0O1o2Kvak5EmxNVAaAJ0oScNk1OkVn+yLKd28MG7AqaSFDkj5E6eSxKyuvDfvLwpzSlyIMvZsx0aoPjrgnlCeTndV7lhKJ6WItOy2U+lJATofkcqXqTBHAAK8MscCIpoUGaNYyx7DtxRLbNe0+S+pNqjcG6Zr91m99Wy9/SzRXfdave6KB9ccdo2q5J3/Uk63p4ahayPf2bFxvkAz1iiZAnj57omLNuF4gO+ie5R7Mki7NfVauYiH0M9MbYvWbwKMvsm37QOggLnAKCmBNlYBohH7N+Fm/xma0gbXMJWks+Ov9SZhooXL64/UDpmvg20t/nEt/CQRRDPEmJK+XbvCdGTsv2sy5UOTtwA7yYANX6b8J9uPxacEmBTqE50g1oqPhrKwrbdv5KIos4HNeUXTZjB6ElXpTjWsKaRmHhinQRRidrXIeeGEW9XPRGiSz0xMKxM4yEbGhYGFpSAfxlF6brpITjfWKaYvPtZUSxCX3RdwJXwHIcUcCI6HWXDq8VqxE8b1G7jd2iXn7gyRdLSQUfYBU7KN1sjD55dISQXkiD/HISeOiuztfV085AaBbDHfIlI5+Oc0O0/QoggsH9eLsO7oajXpCsR3QV8rKlPxxWaMO+M";
            var sut = serviceProvider.GetRequiredService<IEncryptionService>();

            List<Customer> customers;
            var result = sut.TryDecrypt<List<Customer>>(encryptedData, out customers);

            Assert.True(result);
            Assert.NotNull(customers);
            Assert.True(customers.Count == 10);


        }


        [Fact]
        public void EncryptAndDecryptSensitiveData()
        {
            string sensitiveData = "I need to keep this message secure";

            string key = Guid.NewGuid().ToString("N");
            byte[] keyByte = Encoding.UTF8.GetBytes(key);

            string iv = Guid.NewGuid().ToString("N");
            byte[] ivByte = Encoding.UTF8.GetBytes(iv,0,16);

            KeyInfo keyInfo = new KeyInfo(keyByte, ivByte);

            IEncryptionService sut = new EncryptionService(keyInfo);

            var encrypted = sut.Encrypt(sensitiveData);

            var decrypted = sut.Decrypt(encrypted);


            Assert.NotNull(decrypted);

            Output.WriteLine(encrypted);
            Output.WriteLine(decrypted);
        }




        private ServiceProvider RegisterServices()
        {
            IServiceCollection services = new ServiceCollection();

            var iConfig = GetIConfigurationBase();

            services.AddSingleton<IConfiguration>(iConfig);
            services.AddSingleton<IEncryptionService, EncryptionService>();
            services.AddSingleton<ICustomerService, CustomerService>();


            UniqueCode uniqueCode = new UniqueCode();

           


            // services.AddSingleton<KeyInfo>( new KeyInfo(uniqueCode.Key, "mEsWQgKRUBtkyFJzZZQGlw=="));
            services.AddSingleton<KeyInfo>(new KeyInfo(uniqueCode.Key, "mEsWQgKRUBtkyFJzZZQGlw=="));

            services.AddDataProtection();

            //services.AddDataProtection()
            //    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
            //    {
            //        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
            //        ValidationAlgorithm = ValidationAlgorithm.HMACSHA512

            //    });

            return services.BuildServiceProvider();
        }

    }
}
