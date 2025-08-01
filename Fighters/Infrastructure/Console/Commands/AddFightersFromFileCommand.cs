using Application.Interfaces;
using Domain.Interfaces.Types;
using Infrastructure.Loaders.FightersLoader;
using IRandomService = Domain.Interfaces.Services.RandomService.IRandomService;

namespace Infrastructure.Console.Commands;

public class AddFightersFromFileCommand( List<IFighter> fighters, IConsoleService console, IRandomService random )
    : ICommand
{
    public void Execute()
    {
        console.Write( "Введите путь к JSON файлу с бойцами: " );
        string? path = console.ReadLine();


        try
        {
            List<IFighter> loaded = FighterLoader.LoadFightersFromJson( path, random );
            List<IFighter> newFighters = new List<IFighter>();

            foreach ( IFighter fighter in loaded )
            {
                bool nameExists = fighters.Any( existing =>
                                      existing.Name.ToLower() == fighter.Name.ToLower() ) ||
                                  newFighters.Any( existing =>
                                      existing.Name.ToLower() == fighter.Name.ToLower() );

                if ( !nameExists )
                {
                    newFighters.Add( fighter );
                }
            }

            int skipped = loaded.Count - newFighters.Count;

            fighters.AddRange( newFighters );

            console.WriteLine( $"Загружено {newFighters.Count} бойцов" );
            if ( skipped > 0 )
                console.WriteLine( $"Пропущено {skipped} бойцов с уже существующими именами" );
        }
        catch ( Exception ex )
        {
            console.WriteLine( $"Ошибка при загрузке бойцов: {ex.Message}" );
        }
    }
}