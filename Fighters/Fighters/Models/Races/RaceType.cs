using System.ComponentModel;

namespace Fighters.Models.Races;

[Description( "Раса бойца" )]
public enum RaceType
{
    [Description( "Человек" )] 
    Human = 0,

    [Description( "Эльф" )] 
    Elf,

    [Description( "Орк" )] 
    Orc,

    [Description( "Гном" )] 
    Gnome,
}