using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PacMan
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            char[,] map = ReadMap("map.txt");
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });

            Console.CursorVisible = false;

            int pacManPositionX = 1;
            int pacManPositionY = 1;
            int score = 0;
            bool isWorking = true;

            while (isWorking == true)
            {
                Console.Clear();

                HandleInput(pressedKey, ref pacManPositionX, ref pacManPositionY, map, ref score);

                Console.ForegroundColor = ConsoleColor.Magenta;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(pacManPositionX, pacManPositionY);
                Console.Write("@");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(38, 0);
                Console.Write($"Score: {score}");

                Thread.Sleep(1000);
            }
        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacManPositionX, ref int pacManPositionY, char[,] map, ref int score)
        {
            int[] directoin = GetDirection(pressedKey);

            int nextPacManPositionX = pacManPositionX + directoin[0];
            int nextPackManPositionY = pacManPositionY + directoin[1];

            char nextCell = map[nextPacManPositionX, nextPackManPositionY];
            char roadSymbol = ' ';
            char scoreSymbol = '.';

            if (nextCell == roadSymbol || nextCell == scoreSymbol)
            {
                pacManPositionX = nextPacManPositionX;
                pacManPositionY = nextPackManPositionY;

                if (nextCell == scoreSymbol)
                {
                    score += 1;
                    map[nextPacManPositionX, nextPackManPositionY] = ' ';
                }
            }
        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] directions = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                directions[1] -= 1;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                directions[1] += 1;
            }
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                directions[0] -= 1;
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                directions[0] += 1;
            }

            return directions;
        }

        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }

                Console.Write("\n");
            }
        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines("map.txt");

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = file[y][x];
                }
            }

            return map;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (string line in lines)
            {
                if (line.Length > maxLength)
                {
                    maxLength = line.Length;
                }
            }

            return maxLength;
        }
    }
}
