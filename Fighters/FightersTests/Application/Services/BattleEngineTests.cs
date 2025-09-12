using Application.Interfaces;
using Application.Services;
using Domain.Interfaces.Types;
using Moq;

namespace FightersTests.Application.Services;

public class BattleEngineTests
{
    private readonly Mock<IConsoleService> _consoleMock = new();
    private readonly Mock<ICommand> _deleteCommandMock = new();

    private BattleEngine CreateEngine( IFighter[] fighters )
    {
        return new BattleEngine( fighters.ToList(), _deleteCommandMock.Object, _consoleMock.Object );
    }

    private Mock<IFighter> CreateFighter( string name, int initiative, int health, bool alive = true )
    {
        Mock<IFighter> mock = new();
        mock.Setup( f => f.IsAlive ).Returns( () => alive && mock.Object.GetCurrentHealth() > 0 );
        mock.Setup( f => f.GetCurrentHealth() ).Returns( () => health );
        mock.Setup( f => f.Attack( It.IsAny<IFighter>() ) )
            .Callback<IFighter>( target =>
            {
                if ( target is Mock<IFighter> { } m )
                    m.Setup( t => t.GetCurrentHealth() ).Returns( 0 );
            } );
        
        return mock;
    }

    private (Mock<IFighter> firstFighter, Mock<IFighter> secondFighter) CreateDefaultFighters()
    {
        Mock<IFighter> firstFighter = CreateFighter( "First Fighter", 10, 100 );
        Mock<IFighter> secondFighter = CreateFighter( "Second Fighter", 5, 100 );
        
        return ( firstFighter, secondFighter );
    }

    [Fact]
    public void StartBattle_NotEnoughFighters_WritesErrorAndReturns()
    {
        // Arrange
        Mock<IFighter> fighter = CreateFighter( "Only One Fighter", 1, 1 );
        BattleEngine engine = CreateEngine( [ fighter.Object ] );

        // Act
        engine.StartBattle();

        // Assert
        _consoleMock.Verify( c => c.WriteLine( It.Is<string>( s => s.Contains( "Недостаточно бойцов" ) ) ),
            Times.Once );
        _deleteCommandMock.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void StartBattle_OneFighterKillsOther_WinnerDefined()
    {
        // Arrange
        (Mock<IFighter> fighter1, Mock<IFighter> fighter2) = CreateDefaultFighters();
        BattleEngine engine = CreateEngine( [ fighter1.Object, fighter2.Object ] );

        // Act
        engine.StartBattle();

        // Assert
        _consoleMock.Verify( c => c.WriteLine( It.Is<string>( s => s.Contains( "побеждает" ) ) ), Times.Once );
        _deleteCommandMock.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void StartBattle_NoDamageFor10Rounds_FightFinishedWithoutWinner()
    {
        // Arrange
        (Mock<IFighter> firstFighter, Mock<IFighter> secondFighter) = CreateDefaultFighters();
        
        firstFighter.Setup(x => x.Attack(It.IsAny<IFighter>()));
        secondFighter.Setup(x => x.Attack(It.IsAny<IFighter>()));

        BattleEngine engine = CreateEngine( [ firstFighter.Object, secondFighter.Object ] );

        // Act
        engine.StartBattle();

        // Assert
        _consoleMock.Verify(c =>
                c.WriteLine(It.Is<string>(s => s.Contains("Конец боя без победителя"))),
            Times.Once);
        _deleteCommandMock.Verify(c => c.Execute(), Times.Once);
    }
}