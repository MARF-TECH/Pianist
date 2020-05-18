using Microsoft.Extensions.DependencyInjection;
using PianistAnalyser.Application.services;

namespace PianistAnalyser.Desktop
{
    static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient<ConsoleApplication>();
            services.AddScoped<IPartitionReader, PartitionCsvFileReader>();

            return services;
        }
    }
}
