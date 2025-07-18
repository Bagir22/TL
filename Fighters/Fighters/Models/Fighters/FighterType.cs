using System.ComponentModel;

namespace Fighters.Models.Fighters;

[Description( "Тип бойца" )]
public enum FighterType
{
    [Description( "Рыцарь" )] 
    Knight = 0,

    [Description( "Мурмиллион" )] 
    Murmillo,

    [Description( "Фракиец" )] 
    Thraex,

    [Description( "Секутор" )] 
    Secutor
}