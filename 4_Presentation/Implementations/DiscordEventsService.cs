using Discord;
using MediatR;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MlkAdmin._2_Application.Events.UserJoined;
using MlkAdmin._2_Application.Events.UserLeft;
using MlkAdmin._2_Application.Events.ModalSubmitted;
using MlkAdmin._2_Application.Events.ButtonExecuted;
using MlkAdmin._2_Application.Events.GuildAvailable;
using MlkAdmin._2_Application.Events.SelectMenuExecuted;
using MlkAdmin._2_Application.Events.UserVoiceStateUpdated;
using MlkAdmin._2_Application.Events.Ready;
using MlkAdmin._2_Application.Events.MessageReceived;
using MlkAdmin._2_Application.Events.ReactionAdded;
using MlkAdmin._2_Application.Events.UserUpdated;
using MlkAdmin._2_Application.Events.SlashCommandExecuted;
using MlkAdmin._3_Infrastructure.Interfaces;
using MlkAdmin.Shared.Results;
using MlkAdmin._4_Presentation.Discord;

namespace MlkAdmin.Presentation.DiscordListeners;

public class DiscordEventsService(
    ILogger<DiscordEventsService> logger,
    IServiceScopeFactory serviceScopeFactory,
    IDiscordService discordService) : IDiscordEventsService
{
    public BaseResult SubscribeOnEvents()
    {
        discordService.DiscordClient.UserJoined += OnUserJoined;
        discordService.DiscordClient.UserLeft += OnUserLeft;
        discordService.DiscordClient.ModalSubmitted += OnModalSubmitted;
        discordService.DiscordClient.ButtonExecuted += OnButtonExecuted;
        discordService.DiscordClient.GuildAvailable += OnGuildAvailable;
        discordService.DiscordClient.UserVoiceStateUpdated += OnUserVoiceStateUpdated;
        discordService.DiscordClient.SelectMenuExecuted += OnSelectMenuExecuted;
        discordService.DiscordClient.Ready += OnReady;
        discordService.DiscordClient.MessageReceived += OnMessageReceived;
        discordService.DiscordClient.ReactionAdded += OnReactionAdded;
        discordService.DiscordClient.GuildMemberUpdated += GuildMemberUpdated;
        discordService.DiscordClient.SlashCommandExecuted += OnSlashCommandExecuted;

        return BaseResult.Success("Подписка на события прошла успешно");
    
    }

    public BaseResult UnsubscribeOnEvents()
    {
        discordService.DiscordClient.UserJoined -= OnUserJoined;
        discordService.DiscordClient.UserLeft -= OnUserLeft;
        discordService.DiscordClient.ModalSubmitted -= OnModalSubmitted;
        discordService.DiscordClient.ButtonExecuted -= OnButtonExecuted;
        discordService.DiscordClient.GuildAvailable -= OnGuildAvailable;
        discordService.DiscordClient.UserVoiceStateUpdated -= OnUserVoiceStateUpdated;
        discordService.DiscordClient.SelectMenuExecuted -= OnSelectMenuExecuted;
        discordService.DiscordClient.Ready -= OnReady;
        discordService.DiscordClient.MessageReceived -= OnMessageReceived;
        discordService.DiscordClient.ReactionAdded -= OnReactionAdded;
        discordService.DiscordClient.GuildMemberUpdated -= GuildMemberUpdated;
        discordService.DiscordClient.SlashCommandExecuted -= OnSlashCommandExecuted;

        return BaseResult.Success("Отписка от событий прошла успешно");
    }

    private async Task OnUserJoined(SocketGuildUser socketGuildUser)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new UserJoined(socketGuildUser));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnUserJoined] Error - {Message}", ex.Message);
        }
    }
    private async Task OnMessageReceived(SocketMessage socketMessage)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new MessageReceived(socketMessage));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnMessageReceived] Error - {Message}", ex.Message);
        }

    }
    private async Task OnUserLeft(SocketGuild socketGuild, SocketUser socketUser)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new UserLeft(socketGuild, socketUser));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnUserLeft] Error - {Message}", ex.Message);
        }
    }
    private async Task OnModalSubmitted(SocketModal socketModal)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new ModalSubmitted(socketModal));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnModalSubmitted] Error - {Message}", ex.Message);
        }
    }
    private async Task OnButtonExecuted(SocketMessageComponent socketMessageComponent)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new ButtonExecuted(socketMessageComponent));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnButtonExecuted] Error - {Message}", ex.Message);
        }
    }
    private async Task OnGuildAvailable(SocketGuild socketGuild)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new GuildAvailable(socketGuild));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnGuildAvailable] Error - {Message}", ex.Message);
        }
    }
    private async Task OnUserVoiceStateUpdated(SocketUser socketUser, SocketVoiceState oldState, SocketVoiceState newState)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new UserVoiceStateUpdated(socketUser, oldState, newState));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnUserVoiceStateUpdated] Error - {Message}", ex.Message);

        }
    }
   
    private async Task OnSelectMenuExecuted(SocketMessageComponent socketMessageComponent)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new SelectMenuExecuted(socketMessageComponent));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnSelectMenuExecuted] Error - {Message}", ex.Message);
        }

    }
    private async Task OnReady()
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new Ready());
        }
        catch (Exception ex)
        {
            logger.LogError("[OnReady] Error - {Message}", ex.Message);
        }
    }
    private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new ReactionAdded(message, channel, reaction));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnReactionAdded] Error - {Message}", ex.Message);
        }
    }
    private async Task GuildMemberUpdated(Cacheable<SocketGuildUser, ulong> oldUserState, SocketGuildUser newUserState)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new GuildMemberUpdated(oldUserState, newUserState));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnUserUpdated] Error - {Message}", ex.Message);

        }
    }
    private async Task OnSlashCommandExecuted(SocketSlashCommand socketSlashCommand)
    {
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new SlashCommandExecuted(socketSlashCommand));
        }
        catch (Exception ex)
        {
            logger.LogError("[OnUserUpdated] Error - {Message}", ex.Message);

        }
    }
}
