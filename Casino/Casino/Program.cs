namespace Casino
{
    public class Program
    {
        private static readonly Random Random = new Random();
        private static readonly int[] WinningNumbers = [ 18, 19, 20 ];

        const int Multiplicator = 1;

        const string GameName = @"
             ######  #####  ####### ######## ###   ##    #####  
            ##   ## ##   ## ##         ##    ## ##  ##  ##   ## 
            ##        ####   #####     ##    ##  ## ##  ##   ## 
            ##       ## ##     ###     ##    ##   ####  ##   ## 
            ##   ##  ##  ##      ##    ##    ##    ###  ##   ## 
             ######  ##   ## ####### ####### ##     ###  #####  
            ";

        public static void Main( string[] args )
        {
            PrintGameName( GameName );

            Player player = InitPlayer();

            Operation? operation = Operation.Initial;

            while ( operation != Operation.Exit )
            {
                operation = ReadOperation();
                HandleOperation( operation, player );
            }
        }

        private static Player InitPlayer()
        {
            bool isPlayerCreated = false;
            Player? player = null;

            do
            {
                Console.Write( "Please enter amount of money: " );
                string? userInput = Console.ReadLine();
                Console.WriteLine();

                if ( int.TryParse( userInput, out int balance ) && balance > 0 )
                {
                    player = new Player( balance );
                    isPlayerCreated = true;
                }
                else
                {
                    Console.WriteLine( $"Invalid value: {userInput}. Please enter a positive number\n" );
                }
            } while ( !isPlayerCreated );

            return player!;
        }

        private static void PrintGameName( string gameName )
        {
            Console.WriteLine( $"{gameName}\n" );
        }

        private static Operation? ReadOperation()
        {
            PrintAvailableOperations();
            Console.Write( "Please enter operation: " );

            string? operationStr = Console.ReadLine();

            bool isParsed = Enum.TryParse( operationStr, out Operation operation );

            return isParsed ? operation : null;
        }

        private static void HandleOperation( Operation? operation, Player player )
        {
            try
            {
                switch ( operation )
                {
                    case Operation.Initial:
                        return;
                    case Operation.Play:
                        PlayGame( player );
                        break;
                    case Operation.CheckBalance:
                        player.PrintBalanceInfo();
                        break;
                    case Operation.Exit:
                        PrintExitInfo( player );
                        break;
                    default:
                        throw new Exception( $"Unsupported operation type: {operation}" );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.Message );
            }
        }

        private static void PrintExitInfo( Player player )
        {
            Console.WriteLine( "You exit the game" );
            player.PrintBalanceInfo();
        }

        private static void PlayGame( Player player )
        {
            if ( player.Balance == 0 )
            {
                Console.WriteLine( "You are too lost to play(((" );
                Environment.Exit( 0 );
            }

            int bet = MakeBet( player );

            Console.WriteLine( $"\nStart new Game! Your bet is {bet}" );

            int randomNumber = Random.Next( 1, 21 );
            Console.WriteLine( $"Your drawn number is {randomNumber}" );

            bool isWin = WinningNumbers.Contains( randomNumber );

            UpdateBalance( player, isWin, bet, randomNumber );
        }

        private static int MakeBet( Player player )
        {
            int bet = 0;
            Console.WriteLine( $"Please enter a bet, less than or equal to your balance {player.Balance}:" );

            while ( !int.TryParse( Console.ReadLine(), out bet ) || !player.CanMakeBet( bet ) )
            {
                Console.WriteLine( $"Invalid bet. Enter positive bet less or equal your balance {player.Balance}:" );
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

        private static void UpdateBalance( Player player, bool isWin, int bet, int randomNumber )
        {
            if ( isWin )
            {
                player.UpdateBalance( bet * ( Multiplicator * ( randomNumber % 17 ) ) );
                Console.WriteLine( "You win the game!" );
            }
            else
            {
                player.UpdateBalance( -bet );
                Console.WriteLine( "You lose the game(((" );
            }

            player.PrintBalanceInfo();
        }
    }
}