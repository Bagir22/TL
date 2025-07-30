namespace CarFactory.Models.Transmissions;

public interface ITransmission
{
    string Name { get; }
    int GearCount { get; }
    int MaxSpeed { get; }
}