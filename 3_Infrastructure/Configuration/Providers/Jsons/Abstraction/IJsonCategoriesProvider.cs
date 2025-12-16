using MlkAdmin._1_Domain.Interfaces.Discord;

namespace MlkAdmin._1_Domain.Interfaces.Providers;

public interface IJsonCategoriesProvider : IJsonProvider, IJsonConfigurableProvider
{
    public ulong AdminCategoryId { get; }
    public ulong ServerCategoryId { get; }
    public ulong GeneralCategoryId { get; }
    public ulong GameCategoryId { get; }
    public ulong AutoCategoryId { get; }
    public ulong ChillCategoryId { get; }
}