using Casino;

const string gameName = @"
     ######  #####  ####### ######## ###   ##    #####  
    ##   ## ##   ## ##         ##    ## ##  ##  ##   ## 
    ##        ####   #####     ##    ##  ## ##  ##   ## 
    ##       ## ##     ###     ##    ##   ####  ##   ## 
    ##   ##  ##  ##      ##    ##    ##    ###  ##   ## 
     ######  ##   ## ####### ####### ##     ###  #####  
    ";

const int multiplicator = 1;

PrintGameName( gameName );

Console.Write( "Please enter amount of money: " );
string balanceStr = Console.ReadLine();
Console.WriteLine();

bool isBalanceParsed = int.TryParse( balanceStr, out int balance );
if ( !isBalanceParsed )
{
    Console.WriteLine( $"Invalid balance value {balanceStr}" );
    return;
}

if ( balance <= 0 )
{
    Console.WriteLine( "You are to poor for this game" );
    return;
}

Operation? operation = Operation.Initial;

while ( operation != Operation.Exit )
{
    operation = ReadOperation();
    HandleOperation( operation, ref balance );
}

static void PrintGameName( string gameName )
{
    Console.WriteLine( gameName );
    Console.WriteLine();
}

static Operation? ReadOperation()
{
    PrintAvailableOperations();
    Console.Write( "Please enter operation: " );

    string operationStr = Console.ReadLine();
    bool isParsed = Enum.TryParse( operationStr, out Operation operation );

    return isParsed ? operation : null;
}

static void HandleOperation( Operation? operation, ref int balance )
{
    switch ( operation )
    {
        case Operation.Initial:
            return;
        case Operation.Play:
            PlayGame( ref balance );
            break;
        case Operation.CheckBalance:
            PrintBalanceInfo( balance );
            break;
        case Operation.Exit:
            PrintExitInfo( balance );
            break;
        default:
            throw new Exception( $"Unsupported operation type: {operation}" );
    }
}

static void PrintBalanceInfo( int balance )
{
    Console.WriteLine( $"Your balance is {balance}\n" );
}

static void PrintExitInfo( int balance )
{
    Console.WriteLine( $"You exit the game\n" );
    PrintBalanceInfo( balance );
}

static void PlayGame( ref int balance )
{
    int bet = MakeBet( balance );

    Console.WriteLine( $"\nStart new Game! Your bet is {bet}" );

    int randomNumber = new Random().Next( 1, 21 );
    Console.WriteLine( $"Your drawn number is {randomNumber}" );

    bool isWin = new int[] { 18, 19, 20 }.Contains( randomNumber );

    UpdateBalance( isWin, ref balance, bet, randomNumber );
}

static int MakeBet( int balance )
{
    int bet = 0;
    Console.WriteLine( $"Please enter a bet, less than or equal to your balance {balance}:" );

    while ( !int.TryParse( Console.ReadLine(), out bet ) || bet <= 0 || bet > balance )
    {
        Console.WriteLine( $"Invalid bet. Enter positive bet less or equal your balance {balance}:" );
    }

    return bet;
}

static void PrintAvailableOperations()
{
    Console.WriteLine( "Available operations:" );
    Console.WriteLine( "1 - Play the game" );
    Console.WriteLine( "2 - Check the balance" );
    Console.WriteLine( "3 - Exit\n" );
}

static void UpdateBalance( bool isWin, ref int balance, int bet, int randomNumber )
{
    if ( isWin )
    {
        balance += bet * ( multiplicator * ( randomNumber % 17 ) );
        Console.WriteLine( $"You win the game!" );
    }
    else
    {
        balance -= bet;
        Console.WriteLine( $"You lose the game(((" );
    }

    PrintBalanceInfo( balance );
}