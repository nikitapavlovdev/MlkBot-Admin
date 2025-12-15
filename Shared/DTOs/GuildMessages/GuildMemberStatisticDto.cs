namespace MlkAdmin.Shared.DTOs.GuildMessages;

public class GuildMemberStatisticDto
{
    public int MessagesAmount { get; set; }
    public int PicturesAmount { get; set; }
    public int Toxicity { get; set; }
    public long VoiceSessionsTimeSpent { get; set; }
    public int DaysSincejoined { get; set; }
    public byte ActivityLevel { get; set; }
}
