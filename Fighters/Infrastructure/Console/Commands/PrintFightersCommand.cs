using Application.Interfaces;
using Domain.Interfaces.Types;
using Infrastructure.Console.Helpers;

namespace Infrastructure.Console.Commands;

public class PrintFightersCommand( List<IFighter> fighters, IConsoleService console ) : ICommand
{
    public void Execute()
    {
        if ( fighters.Count == 0 )
        {
            console.WriteLine( "Нет добавленных бойцов" );

            return;
        }

        console.WriteLine( "Добавленные бойцы:" );
        foreach ( IFighter fighter in fighters )
        {
            console.WriteLine( $"Боец: {fighter.Name}\n" +
                               $" -> Раса: {EnumPrinter.GetEnumValueDescription( fighter.GetRace().RaceType )}\n" +
                               $" -> Броня: {EnumPrinter.GetEnumValueDescription( fighter.GetArmor().ArmorType )}\n" +
                               $" -> Оружие: {EnumPrinter.GetEnumValueDescription( fighter.GetWeapon().WeaponType )}\n" +
                               $" -> Урон: {fighter.CalculateDamage()}\n" +
                               $" -> Защита: {fighter.CalculateDefense()}\n" +
                               $" -> Инициатива: {fighter.Initiative}\n" );
        }

        console.WriteLine();
    }
}