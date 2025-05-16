namespace classwork
{
    //Создайте приложение «Крестики-Нолики». Пользователь играет с компьютером.
    //При старте игры случайным образом выбирается, кто ходит первым. Игроки ходят по очереди.
    //Игра может закончиться победой одного из игроков или ничьей. Используйте механизмы пространств имён.

    internal class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            board.SetSign(Sign.O, new Coord { y = 1, x = 1 });

            PrintBoard(board);
        }

        public static void PrintBoard(Board board)
        {
            Console.WriteLine(
                $"""
                {(char)board[0, 0]}|{(char)board[0, 1]}|{(char)board[0, 2]}
                =====
                {(char)board[1, 0]}|{(char)board[1, 1]}|{(char)board[1, 2]}
                =====
                {(char)board[2, 0]}|{(char)board[2, 1]}|{(char)board[2, 2]}
                """);
        }
    }
}
