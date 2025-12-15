namespace MlkAdmin._1_Domain.Interfaces.Providers;

public interface IJsonConfigurableProvider
{
    string Path { get; }
    bool IsLoaded { get; }
}
