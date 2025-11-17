using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces.Users;
using Discord.WebSocket;

namespace MlkAdmin._2_Application.Managers.Users.Stat;

public class UserStatManager(
    ILogger<UserStatManager> logger, 
    IUserMessageRepository userMessageRepository,
    IUserVoiceSessionRepository userVoiceSessionRepository)
{
    public async Task UpdateMessageStatAsync(ulong userId, SocketMessage? message)
    {
        try
        {
            UserMessagesStat messageStat = new()
            {
                TotalMessageCount = 1,
                LastUpdate = DateTime.UtcNow,
                UserId = userId
            };

            ModifyStats(message, messageStat);

            await userMessageRepository.AddOrUpdateAsync(messageStat);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при попытке увеличить счетчик пользователя");
        }
    }

    public async Task TrackUserVoiceSessionsAsync(ulong userId, SocketVoiceState newState, SocketVoiceState oldState)
    {
        try
        {
            if (oldState.VoiceChannel != null && newState.VoiceChannel == null)
            {
                await userVoiceSessionRepository.AddOrUpdateAsync(new UserVoiceSession()
                {
                    UserId = userId,
                    VoiceStarting = null
                });
            }

            if (oldState.VoiceChannel == null && newState.VoiceChannel != null)
            {
                await userVoiceSessionRepository.AddOrUpdateAsync(new UserVoiceSession()
                {
                    UserId = userId,
                    VoiceStarting = DateTime.UtcNow
                });
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при попытке отследить голосовую сессию");
        }
    }

    private void ModifyStats(SocketMessage message, UserMessagesStat messagesStat)
    {
        try
        {
            if (message.Attachments.Count > 0)
            {
                foreach (var attachment in message.Attachments)
                {
                    if (attachment.Filename.EndsWith(".gif"))
                    {
                        messagesStat.GifsAmount += 1;
                    }

                    if (attachment.Filename.EndsWith(".png") || attachment.Filename.EndsWith(".jpg"))
                    {
                        messagesStat.PicturesAmount += 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при попытке проанализировать входящее сообщение");
        }
    }
}
