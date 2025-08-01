using System.ComponentModel;

namespace Domain.Types;

[Description( "Броня бойца" )]
public enum ArmorType
{
    [Description( "Без брони" )] 
    NoArmor = 0,

    [Description( "Кожаная броня" )] 
    Leather,

    [Description( "Железная броня" )] 
    Iron
}