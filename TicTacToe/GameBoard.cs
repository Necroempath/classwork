using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public struct Coord
    {
        public int x, y;
    }
}

namespace TicTacToe.Board
{
    public static class SignExtensions
    {
        public static char ToChar(this Sign sign) => sign switch
        {
            Sign.X => 'X',
            Sign.O => 'O',
            Sign.Empty => ' ',
            _ => '?'
        };
    }

    public static class CoordinatesExtensions
    {
        public static bool IsEven(this Coord coord) => coord.y + coord.x % 2 == 0;
    }
    
    public enum MoveResult
    {
        OccupiedPosition,
        PositionOutOfBorders,
        Success
    }

    public enum Sign
    {
        Empty = 2,
        X = 0,
        O = 1
    }
    
    public class GameBoard
    {
        public int Rows { get; }
        public int Columns { get; }
        
        public Sign[,] Board { get; } 

        public GameBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            
            Board = new Sign[rows, columns];
            
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Board[i, j] = Sign.Empty;
                }
            }
        }
        
        public bool ChechBorders(Coord coord)
        {
            if (coord.y >= Rows || coord.y < 0 || coord.x >= Columns || coord.x < 0)
            {
                return false;
            }

            return true;
        }

        public MoveResult SetSign(Sign sign, Coord coord)
        {
            if (!ChechBorders(coord))
            {
                return MoveResult.PositionOutOfBorders;

            }

            if (Board[coord.y, coord.x] != Sign.Empty)
            {
                return MoveResult.OccupiedPosition;
            }

            Board[coord.y, coord.x] = sign;

            return MoveResult.Success;
        }

        public Sign this[int y, int x]
        {
            get => Board[y, x];
        }
    }
}
