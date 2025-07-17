namespace OrderManager;

class Program
{
    private const int DelayToDelivery = 3;
    
    static void Main( string[] args )
    {
        Run();
    }

    private static void PrintConfirmedOrder( OrderData order )
    {
        Console.WriteLine( $"{order.Username}! " +
                           $"Ваш заказ {order.ItemName} в количестве {order.ItemQuantity} оформлен! " +
                           $"Ожидайте доставку по адресу {order.UserAddress} к {DateTime.Now.AddDays( DelayToDelivery ).Date.ToShortDateString()}" );
    }

    private static bool IsOrderConfimerd( OrderData order )
    {
        Console.WriteLine( $"Здравствуйте, {order.Username}, " +
                           $"вы заказали {order.ItemQuantity} {order.ItemName} " +
                           $"на адрес {order.UserAddress}, все верно?" );

        return AskUser( "Заказ правильный? Введите: Да/Нет" );
    }

    private static OrderData CreateOrder()
    {
        OrderData order = new OrderData();

        Console.WriteLine( "Заполните данные для заказа:" );

        order.ItemName = GetStringFromUser( "Название товара" );
        order.ItemQuantity = GetIntFromUser( "Количество товара" );
        order.Username = GetStringFromUser( "Имя пользователя" );
        order.UserAddress = GetStringFromUser( "Адрес доставки" );

        return order;
    }

    private static string GetStringFromUser( string messageToUser )
    {
        string userInput = "";

        while ( string.IsNullOrWhiteSpace( userInput ) )
        {
            Console.Write( $"{messageToUser}: " );
            userInput = Console.ReadLine();
        }

        return userInput;
    }

    private static int GetIntFromUser( string messageToUser )
    {
        int number;

        while ( true )
        {
            Console.Write( $"{messageToUser}: " );
            string userInput = Console.ReadLine();

            if ( int.TryParse( userInput, out number ) && number >= 0 )
            {
                return number;
            }
        }
    }

    private static bool AskUser( string messageToUser )
    {
        Console.WriteLine( messageToUser );
        while ( true )
        {
            string userInput = Console.ReadLine();

            if ( userInput.ToLower() == "да" )
            {
                return true;
            }
            else if ( userInput.ToLower() == "нет" )
            {
                return false;
            }
        }
    }

    private static void Run()
    {
        Console.WriteLine( "Создание нового заказа" );

        OrderData order = CreateOrder();

        if ( IsOrderConfimerd( order ) )
        {
            PrintConfirmedOrder( order );
        }
        else
        {
            Console.WriteLine( "Заказ не принят" );
            if ( AskUser( "Продолжить создание заказа? Введите: Да/Нет" ) )
            {
                Run();
            }
        }
    }
}