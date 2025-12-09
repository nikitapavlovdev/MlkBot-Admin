using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlkAdmin._1_Domain.Entities;

[Table("voice_channels")]
public class GuildVoiceChannel
{
    [Key]
    public int Id { get; set; }


    [Required]
    [Column("discord_id")]
    public ulong DiscordId { get; set; }


    [Required]
    [Column("name")]
    public string Name { get; set; } = string.Empty;


    [Column("category")]
    public string? Category { get; set; }


    [Required]
    [Column("is_gen")]
    public bool IsGen { get; set; }


    [Required]
    [Column("is_temp")]
    public bool IsTemp { get; set; }

}