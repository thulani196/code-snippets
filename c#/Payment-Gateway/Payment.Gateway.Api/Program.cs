using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Payment.Gateway.Logic.Helpers;

namespace Payment.Gateway.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // Get the KeyVault URI from appsettings
                    var configuration = config.Build();
                    var secretUri = configuration["VaultUrl"];
                    // Get the MSI token
                    var azureServiceTokenProvider = new AzureServiceTokenProvider("RunAs=App");

                    //Get the Key Vault client.The client access Key Vault using MSI
                    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

                    //Load all secrets with prefixes of Portal.
                    config.AddAzureKeyVault(secretUri, keyVaultClient, new PrefixKeyVaultSecretManager("Dev"));

                })
                .UseStartup<Startup>();
                //.UseUrls($"http://*:5200");
    }
}
