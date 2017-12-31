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

        private static readonly char[] Symbols = {'~', '+', '^', '.', 'o'};

        private static readonly ConsoleColor[] Colors =
            {ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Green};

        private const string Label = "Xmas Tree 2018";
        private const string Author = "@emilg1101";

        private const int WindowWidth = 80;
        private const int WindowHeight = 24;

        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Xmas Tree";
            Console.SetWindowSize(WindowWidth, WindowHeight);
            
            _random = new Random();
            _snowflakes = new List<Snowflake>();
            _tree = new List<Tree>();
            
            CreateTree();
            DrawTree();

            for (var i = 0; i < 60; i++)
            {
                _snowflakes.Add(NewSnowflake(false));
            }

            while (true)
            {
                SnowFalling();
                DrawTree();
                Thread.Sleep(110);
            }
        }

        public static void CreateTree()
        {
            var offset = 5;
            var endOfTree = offset + 21;
            for (var i = 21; i >= 17; i--)
            {
                for (var j = offset; j < endOfTree; j++)
                {
                    _tree.Add(new Tree(j, i));
                }
                offset++;
                endOfTree--;
            }

            offset = 8;
            endOfTree = offset + 15;
            
            for (var i = 16; i >= 12; i--)
            {
                for (var j = offset; j < endOfTree; j++)
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
                for (var j = offset; j < endOfTree; j++)
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
                    var symbol = Symbols[_random.Next(0, Symbols.Length)];
                    var color = Colors[_random.Next(0, Colors.Length)];
                    Print(tree.X, tree.Y, symbol, color);
                }
                else
                {
                    Print(tree.X, tree.Y, 'o', ConsoleColor.Green);
                }
            }
            Print(WindowWidth-Label.Length, 0, Label, ConsoleColor.Yellow);
            Print(WindowWidth-Author.Length, 1, Author, ConsoleColor.Cyan);
            Print(15, 5, "|", ConsoleColor.Red);
            Print(14, 6, "\\|/", ConsoleColor.Red);
            Print(13, 7, "__*__", ConsoleColor.Red);
            Print(13, 22, "|   |", ConsoleColor.Green);
            Print(2, 23, "_  _ __ ___|___|__ __ _  ___ __ _ _   __ _ ___ __ ___  _ _  _ __ ___ _ __ __ _", ConsoleColor.Green);   
        }

        public static Snowflake NewSnowflake(bool isNewSnowflake)
        {
            var x = _random.Next(1, WindowWidth - 2);
            var y = _random.Next(1, WindowHeight - 2);
            return new Snowflake(x, isNewSnowflake ? 0 : y);
        }

        public static void SnowFalling()
        {
            for (var i = 0; i < _snowflakes.Count; i++)
            {
                var snowflake = _snowflakes[i];
                if (snowflake.X >= 0 && snowflake.X <= WindowWidth - 1)
                {
                    Print(snowflake.X, snowflake.Y, '\u0020', ConsoleColor.White);
                }
                
                var offset = _random.Next(-1, 3);
                if (offset == 0) snowflake.X--;
                if (offset == 2) snowflake.X++;
                if (snowflake.Y < WindowHeight - 2)
                {
                    snowflake.Y++;
                }
                else
                {
                    _snowflakes.RemoveAt(i);
                    _snowflakes.Add(NewSnowflake(true));
                    continue;
                }
                if (snowflake.X >= 0 && snowflake.X <= WindowWidth - 1)
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