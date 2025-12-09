namespace MlkAdmin._1_Domain.Extension;

public static class StringExtensions
{
    public static ulong? ConvertId(this string uid)
    {
        if (string.IsNullOrEmpty(uid) || !ulong.TryParse(uid, out ulong result)) return null;

        return result;
    }
}
