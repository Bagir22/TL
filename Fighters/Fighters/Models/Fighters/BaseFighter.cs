using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public abstract class BaseFighter : IFighter
{
    private static readonly Random _random = new();

    private readonly IRace _race;
    private IArmor? _armor;
    private IWeapon? _weapon;
    private int _currentHealth;

    public string Name { get; }
    public int Initiative { get; }

    protected BaseFighter( string name, IRace race )
    {
        Name = name;
        _race = race;
        _currentHealth = GetMaxHealth();
        Initiative = CalculateInitiative();
    }

    protected virtual int GetClassDamage() => 0;
    protected virtual int GetClassArmor() => 0;
    protected virtual int GetClassHealth() => 0;
    protected virtual int GetClassInitiative() => 0;

    private int CalculateInitiative() => GetRace().Initiative + GetClassInitiative();

    public int GetCurrentHealth() => _currentHealth;
    public int GetMaxHealth() => _race.Health + GetClassHealth();

    public IRace GetRace() => _race;
    public IFighter GetFighterType() => this;

    public IArmor GetArmor() => _armor ?? throw new NullReferenceException( "Для бойца не выбрана броня" );
    public IWeapon GetWeapon() => _weapon ?? throw new NullReferenceException( "Для бойца не выбрано оружие" );

    public int CalculateDamage() => GetWeapon().Damage + GetRace().Damage + GetClassDamage();

    public int CalculateDefense() => GetArmor().Armor + GetRace().Vitality + GetClassArmor();

    public void SetArmor( IArmor armor ) => _armor = armor;

    public void SetWeapon( IWeapon weapon ) => _weapon = weapon;

    public bool IsAlive => GetCurrentHealth() > 0;

    public void TakeDamage( int damage )
    {
        _currentHealth = Math.Max( _currentHealth - damage, 0 );
        Console.WriteLine( $"{Name} получает {damage} урона. " +
                           $"Осталось здоровья: {GetCurrentHealth()}" );

        if ( GetCurrentHealth() <= 0 )
        {
            Console.WriteLine( $"Боец {Name} погибает!" );
        }
    }

    public virtual void Attack( IFighter target )
    {
        int baseDamage = CalculateDamage();

        int randomizedDamage = RandomizeDamage( baseDamage );

        bool isCritical = IsCriticalHit();
        if ( isCritical )
        {
            randomizedDamage *= 2;
        }

        int finalDamage = Math.Max( randomizedDamage - target.CalculateDefense(), 0 );

        Console.WriteLine(
            $"{Name}{( isCritical ? " критически" : "" )} атакует {target.Name}, нанося {finalDamage} урона" );
        
        target.TakeDamage( finalDamage );
    }

    protected int RandomizeDamage( int baseDamage )
    {
        const double minMultiplier = 0.8;
        const double maxMultiplier = 1.1;

        double finalMultiplier = _random.NextDouble() * ( maxMultiplier - minMultiplier ) + minMultiplier;

        return ( int )Math.Round( baseDamage * finalMultiplier );
    }

    protected bool IsCriticalHit()
    {
        return _random.NextDouble() < 0.10;
    }
}