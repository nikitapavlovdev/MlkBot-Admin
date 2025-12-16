namespace MlkAdmin.Shared.DTOs.DiscordComponents;

public class SelectionMenuConfigDto
{
    public string Placeholder { get; set; } = string.Empty;
    public string CustomId { get; set; } = string.Empty;
    public List<SelectOptionConfigDto> Options { get; set; } = [new() { Label = "базовое меню.. ", Value = "base_menu" }];
}

public class SelectOptionConfigDto
{
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
