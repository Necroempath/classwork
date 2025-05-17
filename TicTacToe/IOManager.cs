using TicTacToe.Board;
namespace TicTacToe.IO;

public class IOManager
{
    public static char OpponentChoice()
    {
        Console.Write("Play with 1 - Bot, 2 - Player:\t");
        
        return Console.ReadKey().KeyChar;
    }
    
    public static string UserInput(bool pvp, string activePlayerName)
    {
        Console.Write($"{(pvp ? activePlayerName + ": " : "")}Enter coordinates of position (e.g 02):\t");
        
        return Console.ReadLine();
    }
    
    public static void PrintBoard(GameBoard gameBoard)
    {
        Console.WriteLine(
            $"""
             ╔═══╦═══╦═══╗
             ║ {gameBoard[0, 0].ToChar()} ║ {gameBoard[0, 1].ToChar()} ║ {gameBoard[0, 2].ToChar()} ║
             ╠═══╬═══╬═══╣
             ║ {gameBoard[1, 0].ToChar()} ║ {gameBoard[1, 1].ToChar()} ║ {gameBoard[1, 2].ToChar()} ║
             ╠═══╬═══╬═══╣
             ║ {gameBoard[2, 0].ToChar()} ║ {gameBoard[2, 1].ToChar()} ║ {gameBoard[2, 2].ToChar()} ║
             ╚═══╩═══╩═══╝
             """);
    }
} 