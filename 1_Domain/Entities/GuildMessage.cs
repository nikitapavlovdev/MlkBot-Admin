using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlkAdmin._1_Domain.Entities;

[Table("guild_messages")]
public class GuildMessage
{
    [Key]
    public int Id { get; set; }


    [Required]
    [Column("sender_discord_id")]
    public ulong DiscordUserId { get; set; }


    [Required]
    [Column("content")]
    public string Content { get; set; } = string.Empty;


    [Required]
    [Column("sent_at")]
    public DateTime? SentAt { get; set; } = DateTime.UtcNow;


    [Required]
    [Column("from_t_channel_id")]
    public ulong TChannelId { get; set; }

}