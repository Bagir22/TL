using CarFactory.Models.Bodies;

namespace CarFactory.Factories;

public abstract class BodyFactory
{
    private static readonly Dictionary<BodyType, Func<IBody>> _bodies = new()
    {
        { BodyType.Hatchback, () => new Hatchback() },
        { BodyType.Convertible, () => new Convertible() },
        { BodyType.Sedan, () => new Sedan() },
        { BodyType.Pickup, () => new Pickup() },
    };

    public static bool HasImplementation( BodyType type ) => _bodies.ContainsKey( type );

    public static IBody Create( BodyType type )
    {
        return _bodies[ type ]();
    }
}