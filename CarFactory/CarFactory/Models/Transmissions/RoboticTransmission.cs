namespace CarFactory.Models.Transmissions;

public class RoboticTransmission : ITransmission
{
    public string Name => "Роботизированная";
    public int GearCount => 7;
    public int MaxSpeed => 200;
}