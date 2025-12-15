using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;
using MlkAdmin._3_Infrastructure.JsonModels.Roles;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider.Abstraction;
using MlkAdmin.Shared.DTOs.GuildData;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider;

public class JsonRolesProvider(string path, ILogger<JsonRolesProvider> logger) : JsonProviderBase<RolesListModel>(path, logger), IJsonRolesProvider
{
    public List<GuildRoleInfo> GuildRoles => GetConfig().GuildRoles;
    public BaseResult<GuildRoleInfo> GetRoleDtoByKey(string key)
    {
		try
		{
			var roles = GetConfig().GuildRoles;

			if (roles is null)
			{
                logger.LogWarning(
                    "Словарь ролей не был загружен");

				return BaseResult<GuildRoleInfo>.Fail(new("105", "Словарь ролей не был загружен", new()));
            }
				

			var role = roles.FirstOrDefault(x => x.Key == key);

			if (role is null)
			{
				logger.LogWarning(
					"Роль не найдена");

                return BaseResult<GuildRoleInfo>.Fail(new("105", "Роль не найдена", new()));
            }

			return BaseResult<GuildRoleInfo>.Success(role);

        }
		catch (Exception exception)
		{
			logger.LogError(
				exception,
				"Ошибка при попытке получить модель роли по ключу \"{key}\"",
				key);

            return BaseResult<GuildRoleInfo>.Fail(new("105", "INTERNAL_ERROR", new()));
        }
    }
}