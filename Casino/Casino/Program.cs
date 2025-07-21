namespace Casino
{
    public class Program
    {
        static readonly Random random = new Random();

        const string gameName = @"
             ######  #####  ####### ######## ###   ##    #####  
            ##   ## ##   ## ##         ##    ## ##  ##  ##   ## 
            ##        ####   #####     ##    ##  ## ##  ##   ## 
            ##       ## ##     ###     ##    ##   ####  ##   ## 
            ##   ##  ##  ##      ##    ##    ##    ###  ##   ## 
             ######  ##   ## ####### ####### ##     ###  #####  
            ";

        const int multiplicator = 1;

        public static void Main( string[] args )
        {
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
                Console.WriteLine( "You are too poor for this game" );
                
                return;
            }

            Operation? operation = Operation.Initial;

            while ( operation != Operation.Exit )
            {
                operation = ReadOperation();
                HandleOperation( operation, ref balance );
            }
        }

        private static void PrintGameName( string gameName )
        {
            Console.WriteLine( gameName );
            Console.WriteLine();
        }

        private static Operation? ReadOperation()
        {
            PrintAvailableOperations();
            Console.Write( "Please enter operation: " );

            string operationStr = Console.ReadLine();

            bool isParsed = Enum.TryParse( operationStr, out Operation operation );

            return isParsed ? operation : null;
        }

        private static void HandleOperation( Operation? operation, ref int balance )
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

        private static void PrintBalanceInfo( int balance )
        {
            Console.WriteLine( $"Your balance is {balance}\n" );
        }

        private static void PrintExitInfo( int balance )
        {
            Console.WriteLine( "You exit the game\n" );
            PrintBalanceInfo( balance );
        }

        private static void PlayGame( ref int balance )
        {
            if ( balance == 0 )
            {
                Console.WriteLine( "You are too lost to play!" );
                Environment.Exit( 0 );
            }

            int bet = MakeBet( balance );

            Console.WriteLine( $"\nStart new Game! Your bet is {bet}" );

            int randomNumber = random.Next( 1, 21 );
            Console.WriteLine( $"Your drawn number is {randomNumber}" );

            bool isWin = new int[] { 18, 19, 20 }.Contains( randomNumber );

            UpdateBalance( isWin, ref balance, bet, randomNumber );
        }

        private static int MakeBet( int balance )
        {
            if ( balance == 0 )
            {
                Console.WriteLine( $"Your balance is {balance}. You can't play the game(((" );
                return 0;
            }

            int bet = 0;
            Console.WriteLine( $"Please enter a bet, less than or equal to your balance {balance}:" );

            while ( !int.TryParse( Console.ReadLine(), out bet ) || bet <= 0 || bet > balance )
            {
                Console.WriteLine( $"Invalid bet. Enter positive bet less or equal your balance {balance}:" );
            }

            return bet;
        }

        private static void PrintAvailableOperations()
        {
            Console.WriteLine( "Available operations:" );
            Console.WriteLine( "1 - Play the game" );
            Console.WriteLine( "2 - Check the balance" );
            Console.WriteLine( "3 - Exit\n" );
        }

        private static void UpdateBalance( bool isWin, ref int balance, int bet, int randomNumber )
        {
            if ( isWin )
            {
                balance += bet * ( multiplicator * ( randomNumber % 17 ) );
                Console.WriteLine( "You win the game!" );
            }
            else
            {
                balance -= bet;
                Console.WriteLine( "You lose the game(((" );
            }

            PrintBalanceInfo( balance );
        }
    }
}