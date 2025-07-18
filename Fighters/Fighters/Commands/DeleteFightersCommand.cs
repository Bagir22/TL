using System.ComponentModel;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Utils;

namespace Fighters.Commands;

public class DeleteFightersCommand( List<IFighter> fighters ) : ICommand
{
    public void Execute()
    {
        if ( fighters.Count == 0 )
        {
            Console.WriteLine( "Нет бойцов для удаления" );
            
            return;
        }

        fighters.Clear();
        Console.WriteLine( "Были удалены все бойцы" );
    }
}