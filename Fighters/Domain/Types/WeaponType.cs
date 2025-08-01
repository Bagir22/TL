using System.ComponentModel;

namespace Domain.Types;

[Description( "Оружие бойца" )]
public enum WeaponType
{
    [Description( "Кулаки" )] 
    Fists = 0,

    [Description( "Меч" )] 
    Sword,

    [Description( "Топор" )] 
    Axe,

    [Description( "Булава" )] 
    Mace
}