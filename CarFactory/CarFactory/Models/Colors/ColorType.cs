using System.ComponentModel;

namespace CarFactory.Models.Colors;

public enum ColorType
{
    [Description("Черный")]
    Black = 0,
    
    [Description("Белый")]
    White,
    
    [Description("Желтый")]
    Yellow,
    
    [Description("Синий")]
    Blue,
    
    [Description("Зеленый")]
    Green,
}