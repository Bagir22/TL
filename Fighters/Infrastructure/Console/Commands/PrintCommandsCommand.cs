using Application.Commands;
using Application.Interfaces;
using Infrastructure.Console.Helpers;

namespace Infrastructure.Console.Commands;

public class ShowCommandsCommand(IConsoleService console) : ICommand
{
    public void Execute()
    {
        console.WriteLine( "Доступные команды:" );
        foreach ( Command cmd in Enum.GetValues( typeof( Command ) ) )
        {
            console.WriteLine( $"{( int )cmd} - {EnumPrinter.GetEnumValueDescription( cmd )}" );
        }

        console.WriteLine();
    }
}