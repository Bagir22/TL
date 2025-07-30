using CarFactory.Factories;
using CarFactory.Models.Bodies;
using CarFactory.Models.Car;
using CarFactory.Models.Colors;
using CarFactory.Models.Engines;
using CarFactory.Models.SteeringWheels;
using CarFactory.Models.Transmissions;
using CarFactory.Utils;
using Spectre.Console;

namespace CarFactory;

class Program
{
    static void Main( string[] args )
    {
        AnsiConsole.MarkupLine( "[bold yellow]Создание машины:[/]" );

        Dictionary<string, EngineType> engineOptions =
            Helper.GetEnumChoicesFromFactory<EngineType>( EngineFactory.HasImplementation );

        Dictionary<string, TransmissionType> transmissionOptions =
            Helper.GetEnumChoicesFromFactory<TransmissionType>( TransmissionFactory.HasImplementation );

        Dictionary<string, BodyType> bodyOptions =
            Helper.GetEnumChoicesFromFactory<BodyType>( BodyFactory.HasImplementation );

        EngineType selectedEngine = PromptHelper.PromptSelection( "Выберите тип двигателя:", engineOptions );

        TransmissionType selectedTransmission =
            PromptHelper.PromptSelection( "Выберите трансмиссию:", transmissionOptions );

        BodyType selectedBody = PromptHelper.PromptSelection( "Выберите тип кузова:", bodyOptions );

        Dictionary<string, ColorType> colorOptions = Helper.GetEnumChoicesFromFactory<ColorType>();
        ColorType selectedColor = PromptHelper.PromptSelection( "Выберите цвет:", colorOptions );

        Dictionary<string, SteeringWheelType> steeringOptions = Helper.GetEnumChoicesFromFactory<SteeringWheelType>();
        SteeringWheelType selectedSteering =
            PromptHelper.PromptSelection( "Выберите расположение руля:", steeringOptions );

        ICar car = Factories.CarFactory.Create(
            selectedEngine,
            selectedTransmission,
            selectedBody,
            selectedColor,
            selectedSteering
        );

        Console.Clear();
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine( "[bold green]Сконфигурированная машина:[/]" );
        AnsiConsole.WriteLine( car.ToString() );
    }
}