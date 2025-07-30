namespace CarFactory.Models.Engines;

public class GasolineEngine : IEngine
{
    public string Name => "Бензиновый";
    public int MaxSpeed => 180;
}