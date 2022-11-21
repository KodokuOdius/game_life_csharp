using System;
using System.Threading;

// Правила: 
// -- в пустой (мёртвой) клетке, с которой соседствуют три живые клетки, зарождается жизнь;

// -- если у живой клетки есть две или три живые соседки,
// то эта клетка продолжает жить; в противном случае 
// (если живых соседей меньше двух или больше трёх) клетка умирает
// («от одиночества» или «от перенаселённости»).

class Program
{
    private const int gameWindowSize = 20;
    private static int[,] gameField = new int[gameWindowSize, gameWindowSize];
    private static int[,] bufferField = gameField;
    private static int[] lifeFactor = new int[2] { 2, 3 };
    private static bool gameOver = false;

    private static void CreateField()
    {
        Random rand = new Random();

        for (int i = 0; i < gameWindowSize; i++)
        {
            for (int j = 0; j < gameWindowSize; j++)
            {
                gameField[i, j] = rand.Next(2); ;
            };
        };
    }
    public static void DrowField()
    {
        Console.Clear();

        for (int i = 0; i < gameWindowSize; i++)
        {
            for (int j = 0; j < gameWindowSize; j++)
            {
                if (gameField[i, j] == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                };
                Console.Write(gameField[i, j]);
                Console.ResetColor();
            };
            Console.WriteLine();
        };
    }

    private static int SumAroundCell(int i, int j)
    {
        int totalSum = 0;

        if (i - 1 >= 0)
        {
            totalSum += gameField[i - 1, j];
        };
        if (i + 1 != gameWindowSize)
        {
            totalSum += gameField[i + 1, j];
        };
        if (j - 1 >= 0)
        {
            totalSum += gameField[i, j - 1];
        };
        if (j + 1 != gameWindowSize)
        {
            totalSum += gameField[i, j + 1];
        };
        if (i - 1 >= 0 && j - 1 >= 0)
        {
            totalSum += gameField[i - 1, j - 1];
        };
        if (i + 1 != gameWindowSize && j + 1 != gameWindowSize)
        {
            totalSum += gameField[i + 1, j + 1];
        };
        if (i - 1 >= 0 && j + 1 != gameWindowSize)
        {
            totalSum += gameField[i - 1, j + 1];
        };
        if (i + 1 != gameWindowSize && j - 1 >= 0)
        {
            totalSum += gameField[i + 1, j - 1];
        };

        return totalSum;
    }

    public static void UpdateField()
    {
        int[,] newField = new int[gameWindowSize, gameWindowSize];

        for (int i = 0; i < gameWindowSize; i++)
        {
            for (int j = 0; j < gameWindowSize; j++)
            {
                int _sum = SumAroundCell(i, j);
                if (gameField[i, j] == 0)
                {
                    if (_sum == 3)
                    {
                        newField[i, j] = 1;
                    }
                    else
                    {
                        newField[i, j] = 0;
                    };
                }
                else
                {
                    if (_sum != 2 && _sum != 3)
                    {
                        newField[i, j] = 0;
                    }
                    else
                    {
                        newField[i, j] = 1;
                    };
                };
            };
        };

        gameOver = newField.Cast<int>().SequenceEqual(gameField.Cast<int>()) || newField.Cast<int>().SequenceEqual(bufferField.Cast<int>());

        bufferField = (int[,])gameField.Clone();

        gameField = (int[,])newField.Clone();
    }
    public static void Main()
    {
        CreateField();

        while (!gameOver)
        {
            DrowField();
            UpdateField();

            Thread.Sleep(200);
        };
        Console.WriteLine("========= Game Over =========");
    }
};