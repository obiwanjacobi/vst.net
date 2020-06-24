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
                        ConsoleOutput.Warning($"Command `vstnet {String.Join(" ", args)}` failed.");
                    }
                }
                catch (Exception e)
                {
                    ConsoleOutput.Error($"Command `vstnet {String.Join(" ", args)}` encountered an error.");
                    ConsoleOutput.Error($"{e}");
                }
            }
            else
            {
                if (args.Length > 0)
                {
                    ConsoleOutput.Warning($"Command `vstnet {String.Join(" | ", args)}` could not be interpreted. The '|' are inserted to indicate how the command line was split into arguments.");
                }
                CommandLineArgs.Help();
            }
        }

        private static void DisplayVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();

            ConsoleOutput.Information($"VST.NET Command Line Interface. Version {assembly.GetName().Version}.");
            ConsoleOutput.Information("Copyright © 2008-2020 Jacobi Software.");
            ConsoleOutput.NewLine();
        }
    }
}
