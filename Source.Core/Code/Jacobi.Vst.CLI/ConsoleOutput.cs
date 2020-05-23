using System;

namespace Jacobi.Vst.CLI
{
    internal static class ConsoleOutput
    {
        public static void Error(string error)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = color;
        }

        public static void Warning(string error)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(error);
            Console.ForegroundColor = color;
        }

        public static void Progress(string error)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(error);
            Console.ForegroundColor = color;
        }

        public static void Help(string error)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(error);
            Console.ForegroundColor = color;
        }
    }
}
