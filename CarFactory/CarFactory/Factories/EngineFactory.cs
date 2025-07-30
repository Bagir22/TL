using CarFactory.Models.Engines;

namespace CarFactory.Factories;

public abstract class EngineFactory
{
    private static readonly Dictionary<EngineType, Func<IEngine>> _engines = new()
    {
        { EngineType.Gasoline, () => new GasolineEngine() },
        { EngineType.Diesel, () => new DieselEngine() },
    };

    public static bool HasImplementation( EngineType type ) => _engines.ContainsKey( type );

    public static IEngine Create( EngineType type )
    {
        return _engines[ type ]();
    }
}