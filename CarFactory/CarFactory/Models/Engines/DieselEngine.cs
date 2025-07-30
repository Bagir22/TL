namespace CarFactory.Models.Engines;

public class DieselEngine : IEngine
{
    public string Name => "Дизельный";
    public int MaxSpeed => 160;
}