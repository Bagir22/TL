namespace OrderManager;

class Program
{
    private const int DelayToDelivery = 3;

    static void Main( string[] args )
    {
        Console.WriteLine( "Создание нового заказа" );

        while ( true )
        {
            OrderData order = CreateOrder();

            if ( IsOrderConfirmed( order ) )
            {
                PrintConfirmedOrder( order );

                break;
            }
            else
            {
                Console.WriteLine( "Заказ не принят" );

                bool tryAgain = AskUser( "Продолжить создание заказа?" );
                if ( !tryAgain )
                {
                    Console.WriteLine( "Выход из программы" );

                    break;
                }
            }
        }
    }

    private static void PrintConfirmedOrder( OrderData order )
    {
        Console.WriteLine( $"{order.Username}! " +
                           $"Ваш заказ {order.ItemName} в количестве {order.ItemQuantity} оформлен! " +
                           $"Ожидайте доставку по адресу {order.UserAddress} к {DateTime.Now.AddDays( DelayToDelivery ).Date.ToShortDateString()}" );
    }

    private static bool IsOrderConfirmed( OrderData order )
    {
        Console.WriteLine( $"Здравствуйте, {order.Username}, " +
                           $"вы заказали {order.ItemQuantity} {order.ItemName} " +
                           $"на адрес {order.UserAddress}, все верно?" );

        return AskUser( "Заказ правильный?" );
    }

    private static OrderData CreateOrder()
    {
        Console.WriteLine( "Заполните данные для заказа:" );

        string itemName = GetStringFromUser( "Название товара" );
        int itemQuantity = GetIntFromUser( "Количество товара" );
        string username = GetStringFromUser( "Имя пользователя" );
        string userAddress = GetStringFromUser( "Адрес доставки" );

        try
        {
            return new OrderData( itemName, itemQuantity, username, userAddress );
        }
        catch ( ArgumentException ex )
        {
            Console.WriteLine( $"Не получилось создать заказ:\n" +
                               $"Ошибка: {ex.Message}" );

            return CreateOrder();
        }
    }

    private static string GetStringFromUser( string messageToUser )
    {
        messageToUser += " (или введите Выход для завершения программы)";

        while ( true )
        {
            Console.Write( $"{messageToUser}: " );
            string? userInput = Console.ReadLine();

            CheckForExit( userInput );

            if ( !string.IsNullOrWhiteSpace( userInput ) )
            {
                return userInput;
            }

            Console.WriteLine( "Ошибка: ввод не должен быть пустым. Попробуйте ещё раз" );
        }
    }

    private static int GetIntFromUser( string messageToUser )
    {
        messageToUser += " (или введите Выход для завершения программы)";

        while ( true )
        {
            Console.Write( $"{messageToUser}: " );
            string? userInput = Console.ReadLine();

            CheckForExit( userInput );

            if ( int.TryParse( userInput, out int number ) && number > 0 )
            {
                return number;
            }

            Console.WriteLine( "Ошибка: введите положительное число" );
        }
    }

    private static void CheckForExit( string? input )
    {
        if ( input?.ToLower() == "выход" )
        {
            Console.WriteLine( "Выход из программы" );
            Environment.Exit( 0 );
        }
    }

    private static bool AskUser( string messageToUser )
    {
        messageToUser += " Введите: Да/Нет или Выход для завершение программы";
        Console.WriteLine( messageToUser );
        while ( true )
        {
            string? userInput = Console.ReadLine();

            CheckForExit( userInput );

            if ( userInput?.ToLower() == "да" )
            {
                return true;
            }
            else if ( userInput?.ToLower() == "нет" )
            {
                return false;
            }
        }
    }
}