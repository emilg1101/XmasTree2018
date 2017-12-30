using System;
using System.Collections.Generic;
using System.Threading;

namespace XmasTree
{
    internal class Program
    {
        public class Tree
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Tree(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        
        public class Snowflake
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Snowflake(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private static Random _random;
        private static List<Snowflake> _snowflakes;
        private static List<Tree> _tree;

        private static readonly char[] _symbols = {'~', '+', '^', '.', 'o'};

        private static readonly ConsoleColor[] _colors =
            {ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Green};

        private static string label = "Xmas Tree 2018";
        private static string author = "@emilg1101";
        
        private static int _windowWidth;
        private static int _windowHeight;
        
        public static void Main(string[] args)
        {
            _windowWidth = Console.WindowWidth;
            _windowHeight = Console.WindowHeight;
            
            _random = new Random();
            _snowflakes = new List<Snowflake>();
            _tree = new List<Tree>();
            
            CreateTree();
            DrawTree();

            for (var i = 0; i < 60; i++)
            {
                _snowflakes.Add(NewSnowflake(false));
            }

            Console.CursorVisible = false;
            Console.Title = "Xmas Tree";
            
            while (true)
            {
                SnowFalling();
                DrawTree();
                Thread.Sleep(110);
            }
        }

        public static void CreateTree()
        {
            int offset = 5;
            int endOfTree = offset + 21;
            for (int i = 21; i >= 17; i--)
            {
                for (int j = offset; j < endOfTree; j++)
                {
                    _tree.Add(new Tree(j, i));
                }
                offset++;
                endOfTree--;
            }

            offset = 8;
            endOfTree = offset + 15;
            
            for (int i = 16; i >= 12; i--)
            {
                for (int j = offset; j < endOfTree; j++)
                {
                    _tree.Add(new Tree(j, i));
                }
                offset++;
                endOfTree--;
            }

            offset = 11;
            endOfTree = offset + 9;
            
            for (int i = 11; i >= 6; i--)
            {
                for (int j = offset; j < endOfTree; j++)
                {
                    _tree.Add(new Tree(j, i));
                }
                offset++;
                endOfTree--;
            }
        }

        public static void DrawTree()
        {
            foreach (var tree in _tree)
            {
                if (_random.Next(-1, 2) == 0)
                {
                    char symbol = _symbols[_random.Next(0, _symbols.Length)];
                    ConsoleColor color = _colors[_random.Next(0, _colors.Length)];
                    Print(tree.X, tree.Y, symbol, color);
                }
                else
                {
                    Print(tree.X, tree.Y, 'o', ConsoleColor.Green);
                }
            }
            Print(_windowWidth-label.Length, 0, label, ConsoleColor.Yellow);
            Print(_windowWidth-author.Length, 1, author, ConsoleColor.Cyan);
            Print(15, 5, "|", ConsoleColor.Red);
            Print(14, 6, "\\|/", ConsoleColor.Red);
            Print(13, 7, "__*__", ConsoleColor.Red);
            Print(13, 22, "|   |", ConsoleColor.Green);
            Print(2, 23, "_  _ __ ___|___|__ __ _  ___ __ _ _   __ _ ___ __ ___  _ _  _ __ ___ _ __ __ _", ConsoleColor.Green);   
        }

        public static Snowflake NewSnowflake(bool isNewSnowflake)
        {
            int x = _random.Next(1, _windowWidth - 2);
            int y = _random.Next(1, _windowHeight - 2);
            return new Snowflake(x, isNewSnowflake ? 0 : y);
        }

        public static void SnowFalling()
        {
            for (var i = 0; i < _snowflakes.Count; i++)
            {
                Snowflake snowflake = _snowflakes[i];
                if (snowflake.X >= 0 && snowflake.X <= _windowWidth - 1)
                {
                    Print(snowflake.X, snowflake.Y, '\u0020', ConsoleColor.White);
                }
                
                int offset = _random.Next(-1, 3);
                if (offset == 0) snowflake.X--;
                if (offset == 2) snowflake.X++;
                if (snowflake.Y < _windowHeight - 2)
                {
                    snowflake.Y++;
                }
                else
                {
                    _snowflakes.RemoveAt(i);
                    _snowflakes.Add(NewSnowflake(true));
                    continue;
                }
                if (snowflake.X >= 0 && snowflake.X <= _windowWidth - 1)
                {
                    Print(snowflake.X, snowflake.Y, '*', ConsoleColor.White);
                }
            }
        }

        public static void Print(int x, int y, char symbol, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }
        
        public static void Print(int x, int y, string text, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
        }
    }
}