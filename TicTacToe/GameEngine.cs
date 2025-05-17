using TicTacToe.Board;
using TicTacToe.IO;
using TicTacToe.Validation;

namespace TicTacToe.Game;

public class GameEngine
{
    private static int _turnsCount = 0;
    private static GameBoard _board = null;
    private static Turn _currentTurn;
    private static bool _gameOver = false;
    private static Winner _winner;
    private static bool PvP; 
    public static int TurnsCount     
    {
        get => _turnsCount;
        set
        {
            _turnsCount = value;
            
            if (_turnsCount >= _board.Rows * _board.Columns)
            {
                _gameOver = true;
                _winner = Winner.Draw;
            }
        } 
    }
    
    public enum Turn
    {
        Player = 1,
        Player2 = 2, 
        None = 0
    }

    public enum Winner
    {
        Player = 1,
        Bot = 2,
        Draw = 0
    }

    private static string GetWinner(Winner winner) => winner switch
    {
        Winner.Player => "Player",
        Winner.Bot => PvP ? "Player2" : "Bot",
        Winner.Draw => "Draw",
    };
    
    public static void Launch()
    {
        _board = new GameBoard(3, 3);
        _currentTurn = (Turn)new Random().Next(1, 3);

        switch (IOManager.OpponentChoice())
        {
            case '1':
                PvP = false;
                break;
            case '2':
                PvP = true;
                break;
            default:
                Console.WriteLine("\nIncorrect choice");
                return;
        }

        Console.WriteLine();
        GameLoop();
    }

    public static void GameLoop()
    {
        while (!_gameOver)
        {
            if (_currentTurn == Turn.Player || (_currentTurn == Turn.Player2 && PvP))
            {
                IOManager.PrintBoard(_board);
            }
            
            if (_currentTurn == Turn.Player)
            {
                
                
                if (CheckWinCondition(PlayerTurn()))
                {
                    _gameOver = true;
                    _winner = Winner.Player;
                }
                
                continue;
            }

            Func<Coord> opponentTurn = PvP ? PlayerTurn : BotTurn;
            
            if (CheckWinCondition(opponentTurn()))
            {
                _gameOver = true;
                _winner = Winner.Bot;
            }
        }
        
        IOManager.PrintBoard(_board);
        GameOver();
    }

    private static void GameOver()
    {
        Console.WriteLine($"Game Over. The winner is {GetWinner(_winner)}");
    }

    private static bool CheckWinCondition(Coord coord)
    {
        if (TurnsCount < 5)
        {
            return false;
        }
        
        Sign winSign = (Sign)(TurnsCount % 2);
        
        if (coord.IsEven()) //Check diagonals if true
        {
            if (_board[0, 0] != Sign.Empty &&
                _board[0, 0] == _board[1, 1] &&
                _board[1, 1] == _board[2, 2])
            {
                return true;
            }
            if (_board[0, 2] != Sign.Empty &&
                _board[0, 2] == _board[1, 1] &&
                _board[1, 1] == _board[2, 0])
            {
                return true;
            }
        }
        
        if (_board[coord.y, 0] != Sign.Empty &&
            _board[coord.y, 0] == _board[coord.y, 1] &&
            _board[coord.y, 1] == _board[coord.y, 2])
        {
            return true;
        }
        if (_board[0, coord.x] != Sign.Empty &&
            _board[0, coord.x] == _board[1, coord.x] &&
            _board[1, coord.x] == _board[2, coord.x])
        {
            return true;
        }
        
        return false;
    }

    private static Coord BotTurn()
    {
        List<Coord> coords = new List<Coord>(_board.Rows * _board.Columns - TurnsCount);

        for (int i = 0; i < _board.Rows; i++)
        {
            for (int j = 0; j < _board.Columns; j++)
            {
                if (_board[i, j] == Sign.Empty)
                {
                    coords.Add(new Coord{y = i, x = j});
                }
            }
        }
        
        Coord coord = coords[new Random().Next(0, coords.Count)];
        
        _board.SetSign((Sign)(TurnsCount % 2), coord);
        TurnsCount++;
        _currentTurn = Turn.Player;

        return coord;
    }

    private static Coord PlayerTurn()
    {
        Coord coord = new Coord();
        
        switch (Validator.InputValidityCheck(IOManager.UserInput(PvP, _currentTurn.ToString()).Trim(), out coord))
        {
            case Validator.InputReslut.Success:
                MoveResult result = _board.SetSign((Sign)(TurnsCount % 2), coord);
                
                switch (result)
                {
                    case MoveResult.Success:
                        TurnsCount++;
                        _currentTurn = Turn.Player2;
                        break;
                    case MoveResult.OccupiedPosition:
                        Console.WriteLine($"Invalid input. Position by coordinates {coord.y},{coord.x} already occupied.");
                        break;
                    case MoveResult.PositionOutOfBorders:
                        Console.WriteLine("Invalid input. Position is outside the board.");
                        break;
                }
                
                break;
            case Validator.InputReslut.IncorrectInput:
                Console.WriteLine("Incorrect input. Enter coords in pattern of '12', where 1 - row index, 2 - column index.");
                break;
        }
        
        return coord;
    }
}