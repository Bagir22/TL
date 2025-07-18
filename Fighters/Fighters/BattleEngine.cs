using Fighters.Commands;
using Fighters.Models.Fighters;

namespace Fighters;

public class BattleEngine( List<IFighter> fighters )
{
    private int GetAliveFightersCount() => fighters.Count( f => f.IsAlive );
    private bool HasEnoughFighters() => GetAliveFightersCount() >= 2;

    const int MaxRoundsWithoutDamage = 10;

    public void StartBattle()
    {
        if ( !HasEnoughFighters() )
        {
            Console.WriteLine( $"Недостаточно бойцов для начала боя\n" +
                               $"Текущее количество бойцов - {fighters.Count}" );

            return;
        }

        int round = 1;
        int roundsWithoutDamage = 0;

        while ( GetAliveFightersCount() > 1 )
        {
            Console.WriteLine( $"Раунд {round++}" );

            List<IFighter> aliveFighters = fighters
                .Where( f => f.IsAlive )
                .OrderByDescending( f => f.Initiative )
                .ToList();

            IFighter attacker = aliveFighters[ 0 ];
            IFighter defender = aliveFighters[ 1 ];

            bool isDamageTaken = MakeAttack( attacker, defender );

            if ( defender.IsAlive )
            {
                isDamageTaken = MakeAttack( defender, attacker );
            }

            roundsWithoutDamage = isDamageTaken ? 0 : roundsWithoutDamage + 1;

            if ( roundsWithoutDamage == MaxRoundsWithoutDamage )
            {
                Console.WriteLine( "Бой затянулся без урона\n" +
                                   "Конец боя без победитля(((" );
                break;
            }

            Console.WriteLine();
        }

        IFighter? winner = fighters.FirstOrDefault( f => f.IsAlive );
        if ( winner != null )
            Console.WriteLine( $"{winner.Name} побеждает!\n" );

        new DeleteFightersCommand( fighters ).Execute();
    }

    private static bool MakeAttack( IFighter attacker, IFighter defender )
    {
        int healthBefore = defender.GetCurrentHealth();
        attacker.Attack( defender );
        int damage = healthBefore - defender.GetCurrentHealth();

        return damage > 0;
    }
}