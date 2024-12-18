//Ваша задача — реализовать консольную игру "крестики-нолики". Вам нужно создать игровое поле, где два игрока могут поочередно делать ходы и ставить свои знаки ("X" или "O") в свободные ячейки поля. Игра должна продолжаться до тех пор, пока один из игроков не выиграет, заполнив вертикали, горизонтали или диагонали тремя своими знаками подряд, или пока не наступит ничья. Игроки для хода указывают строку и столбец.

using System;

class Program
{
    static char[,] board = new char[3, 3]; // Игровое поле 3x3
    static char currentPlayer = 'X'; // Первый игрок начинает с 'X'

    static void Main(string[] args)
    {
        InitializeBoard();
        while (true)
        {
            DrawBoard();
            PlayerMove();
            if (CheckWin())
            {
                DrawBoard();
                Console.WriteLine($"Игрок {currentPlayer} выиграл!");
                break;
            }
            if (IsDraw())
            {
                DrawBoard();
                Console.WriteLine("Игра завершилась вничью!");
                break;
            }
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X'; // Смена игрока
        }
    }

    static void InitializeBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' '; // Инициализация поля пустыми символами
            }
        }
    }

    static void DrawBoard()
    {
        Console.Clear(); // Очистка консоли
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("  {0} | {1} | {2}", board[i, 0], board[i, 1], board[i, 2]);
            if (i < 2) Console.WriteLine(" ----------- ");
        }
    }

    static void PlayerMove()
    {
        int row, col;
        while (true)
        {
            Console.WriteLine($"Игрок {currentPlayer}, введите строку и столбец (0, 1 или 2): ");
            string input = Console.ReadLine();
            var parts = input.Split(',');

            if (parts.Length == 2 &&
                int.TryParse(parts[0], out row) &&
                int.TryParse(parts[1], out col) &&
                row >= 0 && row < 3 && col >= 0 && col < 3 &&
                board[row, col] == ' ')
            {
                board[row, col] = currentPlayer; // Установка знака игрока в выбранную ячейку
                break; // Выйти из цикла, если ход успешен
            }
            else
            {
                Console.WriteLine("Неверный ввод. Попробуйте снова.");
            }
        }
    }

    static bool CheckWin()
    {
        // Проверка горизонталей, вертикалей и диагоналей
        for (int i = 0; i < 3; i++)
        {
            // Проверка горизонталей
            if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) return true;
            // Проверка вертикалей
            if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer) return true;
        }
        // Проверка диагоналей
        if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) return true;
        if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer) return true;

        return false;
    }

    static bool IsDraw()
    {
        foreach (var cell in board)
        {
            if (cell == ' ')
                return false; // Если есть хоть одна пустая ячейка, ничьи нет
        }
        return true; // Все ячейки заполнены
    }
}
