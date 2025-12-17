using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Discord;

namespace MlkAdmin._4_Presentation.Extensions
{
    public static class JsonProviderServiceCollectionExtensions
    {

        public static IServiceCollection AddJsonProvider<TInterface, TImplementation>(
            this IServiceCollection services,
            string filePath)
            where TImplementation  : class, TInterface, IJsonProvider
            where TInterface : class
        {
            services.AddScoped<TInterface>(serviceProvider =>
            {
                string fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath));

                if (!File.Exists(fullPath))
                    throw new FileNotFoundException($"Файл не найден: {fullPath}");

                var logger = serviceProvider.GetRequiredService<ILogger<TImplementation>>();

                return (TInterface)Activator.CreateInstance(typeof(TImplementation), fullPath, logger)!;
            });

            return services;
        }
    }
}
