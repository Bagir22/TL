using Application.Interfaces;

namespace Infrastructure.Console.Commands;

public class PlayCommand : ICommand
{
    private readonly IBattleEngine _battleEngine;

    public PlayCommand( IBattleEngine battleEngine )
    {
        _battleEngine = battleEngine;
    }

    public void Execute()
    {
        _battleEngine.StartBattle();
    }
}