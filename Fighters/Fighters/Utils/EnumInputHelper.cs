namespace Fighters.Utils;

public static class EnumInputHelper
{
    public static TEnum GetValidEnumInput<TEnum>() where TEnum : Enum
    {
        while ( true )
        {
            string? input = Console.ReadLine();

            if ( int.TryParse( input, out int choice ) &&
                 Enum.IsDefined( typeof( TEnum ), choice ) )
            {
                TEnum value = ( TEnum )( object )choice;

                if ( EnumImplementationChecker.HasImplementation( value ) )
                    return value;
            }

            Console.WriteLine( "Некорректный или не реализованный ввод. " +
                               "Попробуйте снова" );
        }
    }
}