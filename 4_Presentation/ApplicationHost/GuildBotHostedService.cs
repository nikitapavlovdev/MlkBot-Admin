using Discord;
using Microsoft.Extensions.Hosting;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._3_Infrastructure.Interfaces;
using MlkAdmin._4_Presentation.Discord;

namespace MlkAdmin.Presentation.PresentationServices;

public class GuildBotHostedService(
    IDiscordEventsService eventsService,
    IJsonProvidersHub providersHub,
    IDiscordService discordService) : IHostedService
{
    public async Task StartAsync(CancellationToken token)
    {
        eventsService.SubscribeOnEvents();

        await discordService.DiscordClient.LoginAsync(TokenType.Bot, providersHub.DiscordConfig.MalenkieApiKey);
        await discordService.DiscordClient.StartAsync();
    }

    public async Task StopAsync(CancellationToken token)
    {
        eventsService.UnsubscribeOnEvents();

        await discordService.DiscordClient.StopAsync();
    }
}