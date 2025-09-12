using Application.Commands;
using Application.Interfaces;

namespace Application.Services;

public class GameManager
{
    private readonly Dictionary<Command, ICommand> _commands;
    private readonly Func<Command> _commandReader;

    public GameManager(Dictionary<Command, ICommand> commands, Func<Command> commandReader)
    {
        _commands = commands;
        _commandReader = commandReader;
    }

    public void Run()
    {
        _commands[Command.PrintCommands].Execute();
        Command? command = null; 
        
        while (command != Command.Exit)
        {
            Console.WriteLine("Введите команду:");
            command = _commandReader();
            
            if (command == Command.Exit)
            {
                Console.WriteLine("До свидания!");
                break;
            }

            if (!_commands.TryGetValue((Command)command, out ICommand? cmd))
            {
                Console.WriteLine("Неизвестная или не реализованная команда");
                continue;
            }

            cmd.Execute();
        }
    }
}