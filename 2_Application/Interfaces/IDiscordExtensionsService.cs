namespace MlkAdmin._3_Infrastructure.Interfaces;

public interface IDiscordExtensionsService
{
    Task<string> GetGuildMemberMentionByIdAsync(ulong memberId);
}
