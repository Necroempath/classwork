using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classwork
{
    public enum Result
    {
        OccupiedPosition,
        PositionOutOfBorders,
        Success
    }

    public enum Sign
    {
        Empty = ' ',
        X = 'X',
        O = '0'
    }

    public struct Coord
    {
        public int x, y;
    }

    class Board
    {
        private const int rows = 3;
        private const int columns = 3;

        private Sign[,] board = new Sign[rows, columns];

        public Board()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    board[i, j] = Sign.Empty;
                }
            }
        }
        private bool ChechBorders(Coord coord)
        {
            if (coord.y >= rows || coord.y < 0 || coord.x >= columns || coord.x < 0)
            {
                return false;
            }

            return true;
        }

        public Result SetSign(Sign sign, Coord coord)
        {
            if (!ChechBorders(coord))
            {
                return Result.PositionOutOfBorders;

            }

            if (board[coord.y, coord.x] != Sign.Empty)
            {
                return Result.OccupiedPosition;
            }

            board[coord.y, coord.x] = sign;

            return Result.Success;
        }

        public Sign this[int y, int x]
        {
            get => board[y, x];
        }
    }
}
