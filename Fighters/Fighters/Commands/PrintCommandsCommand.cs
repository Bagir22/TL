using Fighters.Utils;

namespace Fighters.Commands;

public class ShowCommandsCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine( "Доступные команды:" );
        foreach ( Command cmd in Enum.GetValues( typeof( Command ) ) )
        {
            Console.WriteLine( $"{( int )cmd} - {EnumPrinter.GetEnumValueDescription( cmd )}" );
        }

        Console.WriteLine();
    }
}