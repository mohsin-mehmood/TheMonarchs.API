using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using TheMonarchs.Client.ConsoleApp.Interfaces;
using TheMonarchs.Client.ConsoleApp.Services;

namespace TheMonarchs.Client.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);



            IConfigurationRoot configuration = builder.Build();

            var servicesCollection = new ServiceCollection();


            ConfigureLogger(configuration);


            ConfigureServices(servicesCollection, configuration);

            try
            {
                using (var serviceProvider = servicesCollection.BuildServiceProvider())
            {
                var monarchService = serviceProvider.GetService<IMonarchService>();
                
                    await CallMonarchAPIEndpoints(monarchService);
                
            }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "");
            }
        }


        static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient<IMonarchService, MonarchService>(cfg =>
            {
                cfg.BaseAddress = new Uri(configuration["AppSettings:MonarchAPIBaseUrl"]);
            });
        }


        static void ConfigureLogger(IConfiguration configuration)
        {
            // Setup Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

        }


        static async  Task CallMonarchAPIEndpoints(IMonarchService monarchService)
        {

            var monarchCount = await monarchService.GetMonarchsCountAsync();


            if (monarchCount.HasValue)
            {
                Console.WriteLine("How many monarchs are there in the list?");
                Console.WriteLine(monarchCount);

            }
            Console.WriteLine("====================================================================================");

            var longestRulingMonarch = await monarchService.GetLongestRulingMonarchAsync();

            if (longestRulingMonarch != null)
            {

                Console.WriteLine("Which monarch ruled the longest and for how long?");
                Console.WriteLine($"{longestRulingMonarch.Name} ruled for {longestRulingMonarch.RulingPeriod}");
            }

            Console.WriteLine("====================================================================================");

            var response = await monarchService.GetLongestRulingHouseAsync();

            if (response != null)
            {

                Console.WriteLine("Which house ruled the longest and for how long?");
                Console.WriteLine($"{response.House} ruled for {response.YearsRuled} years");
            }


            Console.WriteLine("====================================================================================");


            var commonNameResponse = await monarchService.GetMostCommonFirstNameAsync();

            if (commonNameResponse != null)
            {
                Console.WriteLine("What was the most common first name?");
                Console.WriteLine($"{commonNameResponse.Name} found {commonNameResponse.TotalFound} times.");
                Console.WriteLine($"{String.Join(Environment.NewLine, commonNameResponse.Occurences)}");
            }
        }
    }
}
