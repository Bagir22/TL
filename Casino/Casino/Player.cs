namespace Casino;

public class Player
{
    public int Balance { get; private set; }

    public Player( int initialBalance )
    {
        if ( initialBalance <= 0 )
            throw new ArgumentException( "Initial balance must be more than 0" );

        Balance = initialBalance;
    }

    public bool CanMakeBet( int bet )
    {
        return bet > 0 && bet <= Balance;
    }

    public void PrintBalanceInfo()
    {
        Console.WriteLine( $"Your balance is {Balance}" );
    }

    public void UpdateBalance( int value )
    {
        Balance += value;
    }
}