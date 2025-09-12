using Application.Interfaces;

namespace Infrastructure.Console.ConsoleService;

public class ConsoleService : IConsoleService
{
    public void Write( string message ) => System.Console.Write( message );
    public void WriteLine( string message ) => System.Console.WriteLine( message );
    public void WriteLine() => System.Console.WriteLine();
    public string? ReadLine() => System.Console.ReadLine();
}