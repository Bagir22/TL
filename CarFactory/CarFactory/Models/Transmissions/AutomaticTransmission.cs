namespace CarFactory.Models.Transmissions;

public class AutomaticTransmission : ITransmission
{
    public string Name => "Автоматическая";
    public int GearCount => 4;
    public int MaxSpeed => 170;
}