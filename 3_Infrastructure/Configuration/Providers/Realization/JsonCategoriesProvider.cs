using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._3_Infrastructure.JsonModels.Categories;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider.Abstraction;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider;

public class JsonCategoriesProvider(
    string path,
    ILogger logger) : JsonProviderBase<RootDiscordCategories>(path, logger), IJsonCategoriesProvider
{
    public ulong AdminCategoryId => GetConfig().Guild.Administration.Id;
    public ulong ServerCategoryId => GetConfig().Guild.Server.Id;
    public ulong GeneralCategoryId => GetConfig().Guild.General.Id;
    public ulong GameCategoryId => GetConfig().Guild.Gaming.Id;
    public ulong AutoCategoryId => GetConfig().Guild.Autolobby.Id;
    public ulong ChillCategoryId => GetConfig().Guild.Chill.Id;
}