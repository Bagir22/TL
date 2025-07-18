using System.ComponentModel;

namespace Fighters.Commands;

public enum Command
{
    [Description( "Показать список комманд" )] 
    PrintCommands = 0,

    [Description( "Добавить бойца из консоли" )] 
    AddFighterFromConsole,

    [Description( "Добавить бойцов из файла" )] 
    AddFightersFromFile,

    [Description( "Запустить игру" )] 
    Play,

    [Description( "Показать список бойцов" )] 
    PrintFightersList,

    [Description( "Удалить всех бойцов" )] 
    DeleteFightersList,

    [Description( "Выход из программы" )] 
    Exit
}