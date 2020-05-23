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

        public static void Warning(string warning)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(warning);
            Console.ForegroundColor = color;
        }

        public static void Progress(string progress)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(progress);
            Console.ForegroundColor = color;
        }

        public static void Help(string help)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(help);
            Console.ForegroundColor = color;
        }

        public static void Information(string information)
        {
            Console.WriteLine(information);
        }

        public static void NewLine(int numberOfNewLines = 1)
        {
            while (numberOfNewLines-- > 0)
            {
                Console.WriteLine();
            }
        }
    }
}
