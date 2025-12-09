using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlkAdmin._1_Domain.Entities;

[Table("voice_sessions")]
public class GuildVoiceSession
{
    [Key]
    public int Id { get; set; }


    [Required]
    [Column("vchannel_discord_id")]
    public ulong VChannelDiscordId { get; set; }


    [Required]
    [Column("name")]
    public string Name { get; set; } = string.Empty;


    [Required]
    [Column("starting_at")]
    public DateTime StartingAt { get; set; }


    [Required]
    [Column("time_in_seconds")]
    public long TotalSeconds { get; set; }

}