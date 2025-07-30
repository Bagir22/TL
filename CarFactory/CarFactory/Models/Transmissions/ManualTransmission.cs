namespace CarFactory.Models.Transmissions;

public class ManualTransmission : ITransmission
{
    public string Name => "Механическая";
    public int GearCount => 5;
    public int MaxSpeed => 180;
}