using CarFactory.Models.Bodies;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringWheels;
using CarFactory.Models.Transmissions;

namespace CarFactory.Models.Car;

public interface ICar
{
    IBody Body { get; }
    IEngine Engine { get; }
    ITransmission Transmission { get; }
    ColorType Color { get; }
    SteeringWheelType SteeringWheel { get; }
}