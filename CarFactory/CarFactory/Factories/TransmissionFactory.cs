using CarFactory.Models.Transmissions;

namespace CarFactory.Factories;

public abstract class TransmissionFactory
{
    private static readonly Dictionary<TransmissionType, Func<ITransmission>> _transmissions = new()
    {
        { TransmissionType.ManualTransmission, () => new ManualTransmission() },
        { TransmissionType.AutomaticTransmission, () => new AutomaticTransmission() },
        { TransmissionType.RoboticTransmission, () => new RoboticTransmission() }
    };

    public static bool HasImplementation( TransmissionType type ) => _transmissions.ContainsKey( type );

    public static ITransmission Create( TransmissionType type )
    {
        return _transmissions[ type ]();
    }
}