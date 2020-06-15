using System;
using Dech.Hal.Banking.Contracts;
using Dech.Hal.Banking.Contracts.Interfaces;
using Dech.Hal.Banking.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Dech.Hal.Banking.Tests
{
    public class ServiceDataProtectionShould : UnitTestBase
    {
        public ServiceDataProtectionShould(ITestOutputHelper output): base(output)
        {
           serviceProvider =  RegisterServices();
        }

        

        [Fact]
        public void GetConfigurationSettings()
        {
            var sut = serviceProvider.GetRequiredService<IConfiguration>();

            string application = sut["AppInfo:Email:SenderEmail"];
            string expected = "support@test.com";

            Assert.Equal(application, expected);

            Output.WriteLine(application);

        }


        [Fact]
        public void EncryptDataUsingDataProtection()
        {
            string sensitiveData = "hash my key is part of the test";

            var sut = serviceProvider.GetRequiredService<IServiceDataProtection>();
            var encrypted = sut.Encrypt(sensitiveData);


            Assert.NotNull(encrypted);

            Output.WriteLine(encrypted);

            //DwQvSin0HsKEUu6HYlBM+uYlL2CoIC8ow3QLHH0mAPh0VxpOyxtTiBghPiuhH0+R

        }


        [Fact]
        public void DecryptDataUsingDataProtection()
        {
            string encrypted = "CfDJ8K3aWspf8pRNgo2HuquMzZl9kKzeSoZUILUFo90ADVzPmf5lJm7xhvvOIjtUZ_-D6ZYELZgj4iup_nPP-Stv6-OxJ4upTPrLL3_wwu7s2OPK298s2YoPoORhK70WYbbuRgP4PUWzO1ebRCgpX9J7j7zuji7xgE3tT0zJMfQX4mlU";

            var sut = serviceProvider.GetRequiredService<IServiceDataProtection>();

            string decrypted;

            var isDectypted = sut.TryDecrypt(encrypted, out decrypted);


            Assert.True(isDectypted);

            Output.WriteLine(decrypted);

            //

        }


        [Fact]
        public void ReturnDifferentGuids()
        {

            Guid guidx = Guid.NewGuid();

            var guid = guidx.ToString("N");
            var guidMax = guidx.ToString();


            Assert.True(!string.IsNullOrEmpty(guid));

            Output.WriteLine(guid);

            Output.WriteLine(guidMax);

        }




        [Fact]
        public void HashProvideString()
        {
            string sensitiveData = "hash my key is part of the test";
            var sut = serviceProvider.GetRequiredService<IServiceDataProtection>();

            var hashed = sut.CalculateHash(sensitiveData);


            var match = sut.CheckMatch(hashed, sensitiveData);


            Assert.True(match);

            Output.WriteLine(sensitiveData);
            Output.WriteLine(hashed);

        }




        private ServiceProvider RegisterServices()
        {
            IServiceCollection services = new ServiceCollection();

            var iConfig = GetIConfigurationBase();

            services.AddSingleton<IConfiguration>(iConfig);
            services.AddSingleton<IServiceDataProtection, ServiceDataProtection>();
            services.AddSingleton<UniqueCode>();

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
