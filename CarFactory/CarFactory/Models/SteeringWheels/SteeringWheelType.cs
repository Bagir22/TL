using System.ComponentModel;

namespace CarFactory.Models.SteeringWheels;

public enum SteeringWheelType
{
    [Description("Слево")]
    LeftSideWheel = 0,
    
    [Description("Справа")]
    RightSideWheel,
}