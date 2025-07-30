using System.ComponentModel;

namespace CarFactory.Models.Engines;

public enum EngineType
{
    [Description("Бензиновый")]
    Gasoline = 0,
    
    [Description("Дизельный")]
    Diesel,
}