using System.ComponentModel;

namespace Fighters.Models.Weapons;

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