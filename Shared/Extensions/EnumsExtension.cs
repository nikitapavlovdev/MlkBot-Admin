using MlkAdmin._1_Domain.Enums;

namespace MlkAdmin.Shared.Extensions;
public static class EnumsExtension
{
    public static bool TryParseCustomId(string customId, out SelectionMenuCustomIds result)
    {
        return Enum.TryParse(customId, false, out result);
    }
}
