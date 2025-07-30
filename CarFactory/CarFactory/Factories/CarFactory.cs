using CarFactory.Models.Bodies;
using CarFactory.Models.Car;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringWheels;
using CarFactory.Models.Transmissions;

namespace CarFactory.Factories;

public static class CarFactory
{
    public static ICar Create(
        EngineType engineType,
        TransmissionType transmissionType,
        BodyType bodyType,
        ColorType color,
        SteeringWheelType steering
    )
    {
        IEngine engine = EngineFactory.Create( engineType );
        ITransmission transmission = TransmissionFactory.Create( transmissionType );
        IBody body = BodyFactory.Create( bodyType );

        return new Car
        {
            Engine = engine,
            Transmission = transmission,
            Body = body,
            Color = color,
            SteeringWheel = steering,
        };
    }
}