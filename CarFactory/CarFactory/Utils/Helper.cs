using System.ComponentModel;
using System.Reflection;

namespace CarFactory.Utils;

public static class Helper
{
    public static Dictionary<string, TEnum> GetEnumChoicesFromFactory<TEnum>(
        Func<TEnum, bool>? hasImplementation = null ) where TEnum : Enum
    {
        Dictionary<string, TEnum> result = new();

        foreach ( TEnum value in Enum.GetValues( typeof( TEnum ) ) )
        {
            if ( hasImplementation != null && !hasImplementation( value ) )
                continue;

            string description = GetEnumValueDescription( value );
            result[ description ] = value;
        }

        return result;
    }

    public static string GetEnumValueDescription<TEnum>( TEnum value ) where TEnum : Enum
    {
        FieldInfo? field = typeof( TEnum ).GetField( value.ToString() );
        DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? value.ToString();
    }
}