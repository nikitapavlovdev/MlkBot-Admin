using System.ComponentModel.DataAnnotations;

namespace MlkAdmin._1_Domain.Entities;

public class UserMessagesStat
{
    [Key]
    public ulong UserId { get; set; }
    public int TotalMessageCount { get; set; } = 0;
    public int CommandsAmount{ get; set; } = 0;
    public int GifsAmount { get; set; } = 0;
    public int PicturesAmount { get; set; } = 0;
    public int BadWordsAmount { get; set; } = 0;
    public DateTime? LastUpdate { get; set; } = DateTime.UtcNow;
}
