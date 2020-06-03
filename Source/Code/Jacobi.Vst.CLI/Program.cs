using System;
using System.Reflection;

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
                    var success = cmdLine.Command.Execute();
                    ConsoleOutput.NewLine();

                    if (success)
                    {
                        ConsoleOutput.Information("Command finished.");
                    }
                    else
                    {
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
            var assembly = Assembly.GetExecutingAssembly();

            ConsoleOutput.Information($"VST.NET Command Line Interface. Version {assembly.GetName().Version.ToString()}.");
            ConsoleOutput.Information("Copyright © 2008-2020 Jacobi Software.");
            ConsoleOutput.NewLine();
        }
    }
}
