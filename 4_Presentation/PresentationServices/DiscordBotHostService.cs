using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MlkAdmin.Presentation.DiscordListeners;
using Microsoft.Extensions.DependencyInjection;
using MlkAdmin._1_Domain.Interfaces.Providers;

namespace MlkAdmin.Presentation.PresentationServices
{
    public class DiscordBotHostService(
        ILogger <DiscordBotHostService> logger,
        IServiceProvider serviceProvider,
        IJsonProvidersHub providersHub,
        DiscordSocketClient discordClient) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            string? ApiKey = providersHub.DiscordConfig.MalenkieApiKey;

            using var scope = serviceProvider.CreateScope();

            DiscordEventsListener discordEventsController = scope.ServiceProvider.GetRequiredService<DiscordEventsListener>();
            discordEventsController.SubscribeOnEvents(discordClient);

            await discordClient.LoginAsync(TokenType.Bot, ApiKey);
            await discordClient.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await discordClient.StopAsync();
        }
    }
}