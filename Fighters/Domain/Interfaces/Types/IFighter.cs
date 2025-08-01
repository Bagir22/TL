namespace Domain.Interfaces.Types;

public interface IFighter
{
    public string Name { get; }
    public int Initiative { get; }
    
    public int GetCurrentHealth();
    
    public IRace GetRace();
    public IArmor GetArmor();
    public IWeapon GetWeapon();
    
    public int CalculateDamage();
    public int CalculateDefense();

    public void SetArmor( IArmor armor );
    public void SetWeapon( IWeapon weapon );
    
    public bool IsAlive { get; }
    public void Attack( IFighter target );
    public void TakeDamage( int damage );
}