using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlkAdmin._1_Domain.Entities;

[Table("guild_member")]
public class GuildMember
{
    [Key]
    public int Id { get; set; }


    [Required]
    [Column("discord_id")]
    public ulong DiscordId { get; set; }


    [Required]
    [Column("display_name")]
    public string DisplayName { get; set; } = string.Empty;


    [MaxLength(20)]
    [Column("tg_name")]
    public string? TgName { get; set; }


    [MaxLength(100)]
    [Column("tg_link")]
    public string? TgLink { get; set; }


    [MaxLength(20)]
    [Column("real_name")]
    public string? RealName {  get; set; }


    [Column("joined_at", TypeName ="smalldatetime")]
    public DateTime JoinedAt { get; set; } = DateTime.MinValue;


    [Column("birthday", TypeName ="smalldatetime")]
    public DateTime Birthday {  get; set; } = DateTime.MinValue;

}