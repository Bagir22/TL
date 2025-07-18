using Fighters.Models.Fighters;

namespace Fighters.Commands;

public class PlayCommand( List<IFighter> fighters ) : ICommand
{
    public void Execute()
    {
        BattleEngine engine = new BattleEngine( fighters );
        engine.StartBattle();
    }
}