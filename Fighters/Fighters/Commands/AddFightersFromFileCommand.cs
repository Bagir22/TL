using Fighters.FightersLoader;
using Fighters.Models.Fighters;

namespace Fighters.Commands;

public class AddFightersFromFileCommand( List<IFighter> fighters ) : ICommand
{
    public void Execute()
    {
        Console.Write( "Введите путь к JSON файлу с бойцами: " );
        string? path = Console.ReadLine();

        try
        {
            List<IFighter> loaded = FighterLoader.LoadFightersFromJson( path );
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

            Console.WriteLine( $"Загружено {newFighters.Count} бойцов" );
            if ( skipped > 0 )
                Console.WriteLine( $"Пропущено {skipped} бойцов с уже существующими именами" );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Ошибка при загрузке бойцов: {ex.Message}" );
        }
    }
}