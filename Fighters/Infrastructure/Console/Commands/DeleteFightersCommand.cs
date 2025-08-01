using Application.Interfaces;
using Domain.Interfaces.Types;

namespace Infrastructure.Console.Commands;

public class DeleteFightersCommand( List<IFighter> fighters, IConsoleService console ) : ICommand
{
    public void Execute()
    {
        if ( fighters.Count == 0 )
        {
            console.WriteLine( "Нет бойцов для удаления" );

            return;
        }

        fighters.Clear();
        console.WriteLine( "Были удалены все бойцы" );
    }
}