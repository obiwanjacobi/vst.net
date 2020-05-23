using System;

namespace Jacobi.Vst.CLI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            DisplayVersion();
            CommandLineArgs cmdLine;

            try
            {
                cmdLine = new CommandLineArgs(args);
            }
            catch (InvalidOperationException e)
            {
                ConsoleOutput.Error(e.Message);
                return;
            }

            if (cmdLine.Command != null)
            {
                try
                {
                    if (!cmdLine.Command.Execute())
                    {
                        ConsoleOutput.NewLine();
                        ConsoleOutput.Error("Command failed.");
                    }
                }
                catch (Exception e)
                {
                    ConsoleOutput.Error("Command failed.");
                    ConsoleOutput.Error($"{e.Message}");
                }
            }
            else
            {
                CommandLineArgs.Help();
            }
        }

        private static void DisplayVersion()
        {
            ConsoleOutput.Information("VST.NET Command Line Interface.");
            ConsoleOutput.Information("Copyright © 2008-2020 Jacobi Software.");
            ConsoleOutput.NewLine();
        }
    }
}
