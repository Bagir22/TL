using CarFactory.Models.Bodies;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringWheels;
using CarFactory.Models.Transmissions;
using CarFactory.Utils;

namespace CarFactory.Models.Car;

public class Car : ICar
{
    public required IBody Body { get; init; }
    public required IEngine Engine { get; init; }
    public required ITransmission Transmission { get; init; }
    public ColorType Color { get; init; }
    public SteeringWheelType SteeringWheel { get; init; }

    private int MaxSpeed => Math.Min( Transmission.MaxSpeed, Engine.MaxSpeed );
    private int GearCount => Transmission.GearCount;

    public override string ToString()
    {
        return $" Машина\n" +
               $"- Двигатель: {Engine.Name} (Макс. скорость: {Engine.MaxSpeed} км/ч)\n" +
               $"- Трансимисия: {Transmission.Name} ({GearCount} передач, Макс. скорость: {Transmission.MaxSpeed} км/ч)\n" +
               $"- Кузов: {Body.Name}\n" +
               $"- Расположение руля: {Helper.GetEnumValueDescription( SteeringWheel )}\n" +
               $"- Цвет: {Helper.GetEnumValueDescription( Color )}\n" +
               $"- Максимальная скорость: {MaxSpeed} км/ч\n" +
               $"- Грузоподьемность {Body.MaxWeight} ";
    }
}