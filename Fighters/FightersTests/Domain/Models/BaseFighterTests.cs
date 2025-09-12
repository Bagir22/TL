using Domain.Interfaces.Services.RandomService;
using Domain.Interfaces.Types;
using Domain.Models.Fighters;
using Moq;

namespace FightersTests.Domain.Models;

public class BaseFighterTests
{
    private class TestFighter( string name, IRace race, IRandomService random ) : BaseFighter( name, race, random )
    {
        protected override int GetClassDamage() => 5;
        protected override int GetClassArmor() => 3;
        protected override int GetClassHealth() => 20;
        protected override int GetClassInitiative() => 2;
    }

    private readonly Mock<IRandomService> _randomMock;
    private readonly Mock<IRace> _raceMock;
    private readonly Mock<IWeapon> _weaponMock;
    private readonly Mock<IArmor> _armorMock;

    public BaseFighterTests()
    {
        _randomMock = new Mock<IRandomService>();
        _raceMock = new Mock<IRace>();
        _weaponMock = new Mock<IWeapon>();
        _armorMock = new Mock<IArmor>();

        _raceMock.Setup( r => r.Health ).Returns( 50 );
        _raceMock.Setup( r => r.Damage ).Returns( 10 );
        _raceMock.Setup( r => r.Vitality ).Returns( 5 );
        _raceMock.Setup( r => r.Initiative ).Returns( 5 );
        _weaponMock.Setup( w => w.Damage ).Returns( 15 );
        _armorMock.Setup( a => a.Armor ).Returns( 10 );
    }

    private TestFighter CreateFighter( string name = "Test Fighter Name" ) =>
        new( name, _raceMock.Object, _randomMock.Object );

    [Fact]
    public void Create_CreateFighterWithDefaultValues_CreatesCorrectly()
    {
        // Arrange && Act
        TestFighter fighter = CreateFighter();

        // Assert
        Assert.Equal( "Test Fighter Name", fighter.Name );
        Assert.Equal( 50 + 20, fighter.GetMaxHealth() );
        Assert.Equal( 50 + 20, fighter.GetCurrentHealth() );
        Assert.Equal( 5 + 2, fighter.Initiative );

        Assert.Throws<NullReferenceException>( () => fighter.GetArmor() );
        Assert.Throws<NullReferenceException>( () => fighter.GetWeapon() );
    }

    [Fact]
    public void Create_CreateFighterWithSetArmorAndWeapon_CreatesCorrectly()
    {
        // Arrange && Act
        TestFighter fighter = CreateFighter();
        fighter.SetArmor( _armorMock.Object );
        fighter.SetWeapon( _weaponMock.Object );

        // Assert
        Assert.Equal( "Test Fighter Name", fighter.Name );
        Assert.Equal( 50 + 20, fighter.GetMaxHealth() );
        Assert.Equal( 50 + 20, fighter.GetCurrentHealth() );
        Assert.Equal( 5 + 2, fighter.Initiative );
        Assert.Equal( 3 + 5 + 10, fighter.CalculateDefense() );
        Assert.Equal( 5 + 15 + 10, fighter.CalculateDamage() );
    }

    [Fact]
    public void TakeDamage_ReducesHealthCorrectly()
    {
        // Arramge
        TestFighter fighter = CreateFighter();

        // Act
        fighter.TakeDamage( 50 );

        // Assert
        Assert.Equal( 20, fighter.GetCurrentHealth() );
        Assert.True( fighter.IsAlive );
    }

    [Fact]
    public void TakeDamage_TakeExtraDamage_HealthCannotBeLowerThanCurrentHealth()
    {
        // Arrange
        TestFighter fighter = CreateFighter();

        // Act
        fighter.TakeDamage( Int32.MaxValue );

        // Assert
        Assert.Equal( 0, fighter.GetCurrentHealth() );
        Assert.False( fighter.IsAlive );
    }

    [Fact]
    public void Attack_AttackerAttackTarget_TargetTakesCorrectDamageAndItNotInfluenseAtAttacker()
    {
        // Arrange
        TestFighter attacker = CreateFighter( "Attacker Fighter Name" );
        attacker.SetWeapon( _weaponMock.Object );
        attacker.SetArmor( _armorMock.Object );

        TestFighter target = CreateFighter( "Target Fighter Name" );
        target.SetWeapon( _weaponMock.Object );
        target.SetArmor( _armorMock.Object );

        _randomMock.Setup( r => r.NextDouble() ).Returns( 1.0 );

        // Act && Assert
        Assert.Equal( 50 + 20, attacker.GetMaxHealth() );
        Assert.Equal( 50 + 20, target.GetMaxHealth() );

        attacker.Attack( target );

        Assert.Equal( 50 + 20, attacker.GetMaxHealth() );
        Assert.True( target.GetCurrentHealth() < target.GetMaxHealth() );
    }

    [Fact]
    public void Attack_CriticalHit_DamageIsDoubled()
    {
        // Arrange
        TestFighter attacker = CreateFighter( "Attacker Fighter Name" );
        attacker.SetWeapon( _weaponMock.Object );

        TestFighter target = CreateFighter( "Target Fighter Name" );
        target.SetArmor( _armorMock.Object );

        int targetInitialHealth = target.GetCurrentHealth();

        _randomMock.SetupSequence( r => r.NextDouble() )
            .Returns( 1.0 )
            .Returns( 0.05 );

        int expectedBaseDamage = attacker.CalculateDamage();
        int expectedRandomized = ( int )Math.Round( expectedBaseDamage * 1.1 );
        int expectedCriticalDamage = expectedRandomized * 2;

        // Act
        attacker.Attack( target );

        int targetHealthAfter = target.GetCurrentHealth();
        int damageToTarget = targetInitialHealth - targetHealthAfter;

        // Assert
        Assert.Equal( expectedCriticalDamage - target.CalculateDefense(), damageToTarget );
    }
}