using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._1_Domain.Interfaces.Providers;
using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider.Abstraction;

public abstract class JsonProviderBase<T>(string path, ILogger logger) : IJsonProvider, IJsonConfigurableProvider where T : class
{
    protected T? _config = null;

    public string Path { get; } = path;
    public bool IsLoaded => _config != null;

    public virtual void Load()
    {
		try
		{
			_config = JsonConvert.DeserializeObject<T>(File.ReadAllText(Path));

			logger.LogInformation("Конфигурация загружена из {path}", Path);
		}
		catch (Exception exception)
		{
			logger.LogError(
				exception,
				"Ошибка при загрузки конфигурации из {Path}",
				Path);

			throw;
		}
    }

	protected T GetConfig() => _config ?? throw new InvalidOperationException("Конфигурация не загружена");
}
