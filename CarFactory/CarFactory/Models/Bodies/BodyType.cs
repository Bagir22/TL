using System.ComponentModel;

namespace CarFactory.Models.Bodies;

public enum BodyType
{
    [Description("Хэтчбек")]
    Hatchback = 0,
    
    [Description("Кабриолет")]
    Convertible,
    
    [Description("Седан")]
    Sedan,
    
    [Description("Пикап")]
    Pickup
}