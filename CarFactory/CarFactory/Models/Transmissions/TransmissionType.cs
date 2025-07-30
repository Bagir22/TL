using System.ComponentModel;

namespace CarFactory.Models.Transmissions;

public enum TransmissionType
{
    [Description( "Механическая" )] 
    ManualTransmission = 0,

    [Description( "Автоматическая" )] 
    AutomaticTransmission,

    [Description( "Роботизированная" )]
    RoboticTransmission,
}