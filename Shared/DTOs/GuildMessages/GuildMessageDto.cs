namespace MlkAdmin.Shared.DTOs.GuildMessages;

public class GuildMessageDto
{
    public string Message { get; set; } = string.Empty;
    public GuildMessageEmbedDto? Embed { get; set; } 
}
