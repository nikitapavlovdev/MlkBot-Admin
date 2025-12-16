using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._1_Domain.Interfaces.Roles;
using MlkAdmin._1_Domain.Interfaces.Services;
using MlkAdmin._1_Domain.Interfaces.Users;
using MlkAdmin._1_Domain.Managers;
using MlkAdmin._2_Application.Events.ButtonExecuted;
using MlkAdmin._2_Application.Events.GuildAvailable;
using MlkAdmin._2_Application.Events.MessageReceived;
using MlkAdmin._2_Application.Events.ModalSubmitted;
using MlkAdmin._2_Application.Events.ReactionAdded;
using MlkAdmin._2_Application.Events.Ready;
using MlkAdmin._2_Application.Events.SelectMenuExecuted;
using MlkAdmin._2_Application.Events.SlashCommandExecuted;
using MlkAdmin._2_Application.Events.UserJoined;
using MlkAdmin._2_Application.Events.UserLeft;
using MlkAdmin._2_Application.Events.UserUpdated;
using MlkAdmin._2_Application.Events.UserVoiceStateUpdated;
using MlkAdmin._2_Application.Interfaces;
using MlkAdmin._2_Application.Managers;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannels;
using MlkAdmin._2_Application.Managers.Messages;
using MlkAdmin._2_Application.Managers.Users;
using MlkAdmin._2_Application.Services.Channels;
using MlkAdmin._2_Application.Services.Messages;
using MlkAdmin._3_Infrastructure.DataBase;
using MlkAdmin._3_Infrastructure.Implementations.Builders;
using MlkAdmin._3_Infrastructure.Implementations.Services;
using MlkAdmin._3_Infrastructure.Interfaces;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using MlkAdmin._3_Infrastructure.Services;
using MlkAdmin._4_Presentation.Discord;
using MlkAdmin._4_Presentation.Extensions;
using MlkAdmin.Presentation.DiscordListeners;
using MlkAdmin.Presentation.PresentationServices;

namespace MlkAdmin.Presentation.DI;

public static class Registration
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        

        services.AddScoped<IGuildChannelsRepository, GuildChannelsRepository>();
        services.AddScoped<IGuildMembersRepository, GuildMembersRepository>();
        services.AddScoped<IGuildMessagesRepository, GuildMessagesRepository>();
        services.AddScoped<IGuildVoiceSessionRepository, GuildVoiceSessionRepository>();

        return services;
    }
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscordEmbedBuilder, DiscordEmbedBuilder>();
        services.AddScoped<IDiscordMessageComponentsBuilder, DiscordMessageComponentsBuilder>();
        services.AddScoped<IDynamicMessageManager, DynamicMessageManager>();
        services.AddScoped<IGuildMembersManager, GuildMembersManager>();
        services.AddScoped<IGuildChannelsService, GuildChannelsService>();
        services.AddScoped<IGuildInitializationService, GuildInitializationService>();
        services.AddScoped<IGuildMessagesService, GuildMessageService>();
        services.AddScoped<IGuildRolesService, GuildRolesService>();

        return services;
    }
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscordService, DiscordService>();

        services.AddDbContext<MlkAdminDbContext>(options =>
        {
            string baseDir = AppContext.BaseDirectory;
            string projectRoot = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName;
            string dbPath = Path.Combine(projectRoot, "mlkadmin.db");

            options.UseSqlite($"Data Source ={dbPath}");
        });

        return services;
    }
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(Startup).Assembly,
            typeof(UserJoinedHandler).Assembly,
            typeof(UserLeftHandler).Assembly,
            typeof(ModalSubmittedHandler).Assembly,
            typeof(ButtonExecutedHandler).Assembly,
            typeof(GuildAvailableHandler).Assembly,
            typeof(UserVoiceStateUpdatedHandler).Assembly,
            typeof(SelectMenuExecutedHandler).Assembly,
            typeof(ReadyHandler).Assembly,
            typeof(MessageReceivedHandler).Assembly,
            typeof(ReactionAddedHandler).Assembly,
            typeof(GuildMemberUpdated).Assembly,
            typeof(SlashCommandExecutedHandler).Assembly));

        services.AddHostedService<DiscordBotHostService>();

        services.AddScoped<DiscordEventsListener>();
        services.AddScoped<CommandService>();
        services.AddScoped<DiscordSlashCommandAdder>();

        services.AddSingleton(new DiscordSocketClient(new() { GatewayIntents = GatewayIntents.All}));

        services.AddJsonProvider<JsonChannelsProvider>("../../../3_Infrastructure/Configuration/DiscordChannelsMap.json");
        services.AddJsonProvider<JsonConfigProvider>("../../../3_Infrastructure/Configuration/DiscordConfiguration.json");
        services.AddJsonProvider<JsonEmotesProvider>("../../../3_Infrastructure/Configuration/DiscordEmotes.json");
        services.AddJsonProvider<JsonPicturesProvider>("../../../3_Infrastructure/Configuration/DiscordPictures.json");
        services.AddJsonProvider<JsonRolesProvider>("../../../3_Infrastructure/Configuration/DiscordRoles.json");
        services.AddJsonProvider<JsonCategoriesProvider>("../../../3_Infrastructure/Configuration/DiscordCategoriesMap.json");
        services.AddJsonProvider<JsonDynamicMessagesProvider>("../../../3_Infrastructure/Configuration/DiscordDynamicMessages.json");

        return services;
    }
}
