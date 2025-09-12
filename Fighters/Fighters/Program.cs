using Application.Commands;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces.Types;
using Infrastructure.Console.Commands;
using Infrastructure.Console.ConsoleService;
using Infrastructure.Console.Helpers;
using Infrastructure.Random.RandomService;

namespace Fighters;

class Program
{
    static void Main(string[] args)
    {
        List<IFighter> fighters = new();
        IConsoleService console = new ConsoleService();
        ICommand deleteFightersCommand = new DeleteFightersCommand(fighters, console);

        IBattleEngine battleEngine = new BattleEngine(fighters, deleteFightersCommand, console);
        
        Dictionary<Command, ICommand> commands = new()
        {
            { Command.PrintCommands, new ShowCommandsCommand(console) },
            { Command.AddFighterFromConsole, new AddFighterFromConsoleCommand(fighters, console) },
            { Command.AddFightersFromFile, new AddFightersFromFileCommand(fighters, console, new RandomService()) },
            { Command.Play, new PlayCommand(battleEngine) },
            { Command.PrintFightersList, new PrintFightersCommand(fighters, console) },
            { Command.DeleteFightersList, new DeleteFightersCommand(fighters, console) }
        };

        EnumImplementationChecker.SetImplementedCommands(commands.Keys.Append( Command.Exit ));
        
        Func<Command> inputParser = () => EnumInputHelper.GetValidEnumInput<Command>();

        GameManager gameManager = new(commands, inputParser);
        gameManager.Run();
    }

}