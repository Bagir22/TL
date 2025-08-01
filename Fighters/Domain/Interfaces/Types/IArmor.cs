using Domain.Types;

namespace Domain.Interfaces.Types;

public interface IArmor
{
    public int Armor { get; }
    public ArmorType ArmorType { get; }
}