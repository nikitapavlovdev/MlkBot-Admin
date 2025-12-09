using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MlkAdmin._1_Domain.Entities;

[Table("text_channels")]
public class GuildTextChannel()
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

}
