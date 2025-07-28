namespace OrderManager;

public class OrderData
{
    public string ItemName { get; private set; }
    public int ItemQuantity { get; private set; }
    public string Username { get; private set; }
    public string UserAddress { get; private set; }

    public OrderData( string itemName, int itemQuantity, string username, string userAddress )
    {
        if ( string.IsNullOrWhiteSpace( itemName ) )
            throw new ArgumentException( "Название товара не может быть пустым" );

        if ( itemQuantity <= 0 )
            throw new ArgumentException( $"Количество товара должно быть больше 0" );

        if ( string.IsNullOrWhiteSpace( username ) )
            throw new ArgumentException( "Имя пользователя не может быть пустым" );

        if ( string.IsNullOrWhiteSpace( userAddress ) )
            throw new ArgumentException( "Адрес доставки не может быть пустым" );

        ItemName = itemName;
        ItemQuantity = itemQuantity;
        Username = username;
        UserAddress = userAddress;
    }
}