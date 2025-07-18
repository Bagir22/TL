using Fighters.Commands;
using Fighters.Models.Fighters;
using Fighters.Utils;

namespace Fighters;

public class GameManager
{
    private readonly List<IFighter> _fighters = new();
    private readonly Dictionary<Command?, ICommand> _commands;

    public GameManager()
    {
        _commands = new Dictionary<Command?, ICommand>
        {
            { Command.PrintCommands, new ShowCommandsCommand() },
            { Command.AddFighterFromConsole, new AddFighterFromConsoleCommand( _fighters ) },
            { Command.AddFightersFromFile, new AddFightersFromFileCommand( _fighters ) },
            { Command.Play, new PlayCommand( _fighters ) },
            { Command.PrintFightersList, new PrintFightersCommand( _fighters ) },
            { Command.DeleteFightersList, new DeleteFightersCommand( _fighters ) }
        };

        EnumImplementationChecker.
            SetImplementedCommands( _commands.Keys.Where( с => с != null ).Cast<Command>().
                Append( Command.Exit ) );
    }

    public void Run()
    {
        Command command = Command.PrintCommands;
        _commands[ command ].Execute();

        while ( command != Command.Exit )
        {
            Console.WriteLine( $"Введите команду: " );
            command = EnumInputHelper.GetValidEnumInput<Command>();

            if ( command == Command.Exit )
            {
                Console.WriteLine( "До свидания!" );
                break;
            }

            if ( !_commands.TryGetValue( command, out ICommand? cmd ) )
            {
                Console.WriteLine( "Неизвестная или не реализованная команда" );
                continue;
            }

            cmd.Execute();
        }
    }
}