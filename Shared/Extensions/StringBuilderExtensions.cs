using System.Text;

namespace MlkAdmin.Shared.Extensions;

public static class StringBuilderExtensions
{
    public static void CompareChange(this StringBuilder stringBuilder, string fieldName, string? oldValue, string? newValue)
    {
        if (oldValue != newValue)
        {
            stringBuilder.AppendLine($"**{fieldName} изменен:**");
            stringBuilder.AppendLine($"> **Старое:** {oldValue ?? "-"}");
            stringBuilder.AppendLine($"> **Новое:** {newValue ?? "-"}");
        }
    }
}
