using System.ComponentModel;
using System.Reflection;

namespace Infrastructure.Console.Helpers;

public static class EnumPrinter
{
    public static void PrintAvailableInputs<TEnum>() where TEnum : Enum
    {
        System.Console.WriteLine( $"Выберите {GetEnumTypeDescription<TEnum>()}:" );

        foreach ( TEnum value in Enum.GetValues( typeof( TEnum ) ) )
        {
            if ( !EnumImplementationChecker.HasImplementation( value ) )
                continue;

            int intValue = Convert.ToInt32( value );
            string description = GetEnumValueDescription( value );
            System.Console.WriteLine( $"{intValue} - {description}" );
        }
    }

    public static string GetEnumValueDescription<TEnum>( TEnum value ) where TEnum : Enum
    {
        FieldInfo? field = typeof( TEnum ).GetField( value.ToString() );
        DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        
        return attribute?.Description ?? value.ToString();
    }

    private static string GetEnumTypeDescription<T>() where T : Enum
    {
        Type type = typeof( T );
        DescriptionAttribute? attr = type.GetCustomAttributes( typeof( DescriptionAttribute ), false )
            .FirstOrDefault() as DescriptionAttribute;

        return attr?.Description ?? type.Name;
    }
}