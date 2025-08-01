using Application.Interfaces;
using Domain.Interfaces.Types;

namespace Application.Services;

public class BattleEngine : IBattleEngine
{
    private readonly List<IFighter> _fighters;
    private readonly IConsoleService _console;
    private readonly ICommand _deleteFightersCommand;

    public BattleEngine( List<IFighter> fighters, ICommand deleteFightersCommand, IConsoleService console )
    {
        _fighters = fighters;
        _deleteFightersCommand = deleteFightersCommand;
        _console = console;
    }

    private int GetAliveFightersCount() => _fighters.Count( f => f.IsAlive );
    private bool HasEnoughFighters() => GetAliveFightersCount() >= 2;

    const int MaxRoundsWithoutDamage = 10;

    public void StartBattle()
    {
        if ( !HasEnoughFighters() )
        {
            _console.WriteLine( $"Недостаточно бойцов для начала боя\nТекущее количество бойцов - {_fighters.Count}" );
            return;
        }

        int round = 1;
        int roundsWithoutDamage = 0;

        while ( GetAliveFightersCount() > 1 )
        {
            _console.WriteLine( $"Раунд {round++}" );

            var aliveFighters = _fighters
                .Where( f => f.IsAlive )
                .OrderByDescending( f => f.Initiative )
                .ToList();

            var attacker = aliveFighters[ 0 ];
            var defender = aliveFighters[ 1 ];

            bool isDamageTaken = MakeAttack( attacker, defender );

            if ( defender.IsAlive )
                isDamageTaken = MakeAttack( defender, attacker );

            roundsWithoutDamage = isDamageTaken ? 0 : roundsWithoutDamage + 1;

            if ( roundsWithoutDamage == MaxRoundsWithoutDamage )
            {
                _console.WriteLine( "Бой затянулся без урона\nКонец боя без победителя((" );
                break;
            }

            _console.WriteLine( "" );
        }

        IFighter? winner = _fighters.FirstOrDefault( f => f.IsAlive );
        if ( winner != null )
            _console.WriteLine( $"{winner.Name} побеждает!\n" );

        _deleteFightersCommand.Execute();
    }

    private bool MakeAttack( IFighter attacker, IFighter defender )
    {
        int healthBefore = defender.GetCurrentHealth();
        attacker.Attack( defender );
        int damage = healthBefore - defender.GetCurrentHealth();

        return damage > 0;
    }
}