using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public interface IFighter
{
    public string Name { get; }
    public int Initiative { get; }
    
    public int GetCurrentHealth();
    public int GetMaxHealth();
    
    public IRace GetRace();
    public IArmor GetArmor();
    public IWeapon GetWeapon();
    public IFighter GetFighterType();
    
    public int CalculateDamage();
    public int CalculateDefense();

    public void SetArmor( IArmor armor );
    public void SetWeapon( IWeapon weapon );
    
    public bool IsAlive { get; }
    public void Attack( IFighter target );
    public void TakeDamage( int damage );
}