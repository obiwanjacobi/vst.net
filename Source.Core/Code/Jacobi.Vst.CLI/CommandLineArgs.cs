using System;
using System.Linq;

namespace Jacobi.Vst.CLI
{
    internal sealed class CommandLineArgs
    {
        private static readonly CommandInfo[] Commands = new[]
        {
            new CommandInfo { Type = typeof(HelpCommand), Name = "help", Description="Displays (this) command usage details." },
            new CommandInfo { Type = typeof(PublishCommand), Name = "publish", Description="Creates a deployment ready to publish.",
                Arguments = new[] {
                    new ArgumentInfo { Property = nameof(PublishCommand.FilePath), Description="The file to publish." },
                    new ArgumentInfo { Property = nameof(PublishCommand.DeployPath), Name = "-o", Description="The output directory that will receive all the files." },
                }
            }
        };

        public static void Help()
        {
            ConsoleOutput.Help("vstnet <command> -<arg1> -<arg2> ...");
            ConsoleOutput.NewLine();

            foreach (var cmd in Commands)
            {
                ConsoleOutput.Help($"{cmd.Name}: {cmd.Description}");
                if (cmd.Arguments != null)
                {
                    foreach (var arg in cmd.Arguments)
                    {
                        if (arg.Name != null)
                        {
                            ConsoleOutput.Help($"\t{arg.Name} - {arg.Description}");
                        }
                        else
                        {
                            ConsoleOutput.Help($"\t1st (unnamed) - {arg.Description}");
                        }
                    }
                }
            }
        }

        public CommandLineArgs(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                Parse(args);
            }
        }

        public ICommand Command { get; set; }

        private void Parse(string[] args)
        {
            var tokens = new Tokens(args);

            while (tokens.MoveNext())
            {
                if (!tokens.IsArgument)
                {
                    Command = ParseCommand(tokens);
                }
                else
                {
                    Ignore(tokens.Current);
                }
            }
        }

        private void Ignore(string ignore)
        {
            Console.WriteLine($"Ignoring command line arg: {ignore}");
        }

        private ICommand ParseCommand(Tokens tokens)
        {
            var cmdInfo = FindCommand(tokens.Current);

            if (cmdInfo != null)
            {
                var cmd = (ICommand)Activator.CreateInstance(cmdInfo.Type);

                if (tokens.MoveNext())
                {
                    ParseArguments(cmdInfo, cmd, tokens);
                }
                return cmd;
            }

            return null;
        }

        private void ParseArguments(CommandInfo cmdInfo, ICommand cmd, Tokens tokens)
        {
            if (cmdInfo.Arguments == null) return;

            var argInfo = cmdInfo.Arguments.FirstOrDefault();

            if (!tokens.IsArgument)
            {
                if (argInfo != null && argInfo.Name == null)
                {
                    // nameless param
                    SetProperty(cmd, argInfo.Property, tokens.Current);
                }
                else
                    throw new InvalidOperationException(
                        $"'{tokens.Current} did not match any argument for the {cmdInfo.Name} command.");
            }

            while (tokens.MoveNext())
            {
                if (!tokens.IsArgument)
                    break;

                argInfo = cmdInfo.Arguments.SingleOrDefault(a => a.Name == tokens.Current);

                if (argInfo != null)
                {
                    if (tokens.MoveNext())
                    {
                        SetProperty(cmd, argInfo.Property, tokens.Current);
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            $"No value specified for argument '{argInfo.Name}' (end of command line).");
                    }
                }
                else
                {
                    throw new InvalidOperationException(
                        $"The {cmdInfo.Name} command does not have an argument '{tokens.Current}'");
                }
            }
        }

        private static void SetProperty(object instance, string property, string value)
        {
            var type = instance.GetType();
            var propInfo = type.GetProperty(property);
            if (propInfo != null)
            {
                var typedValue = Convert.ChangeType(value, propInfo.PropertyType);
                propInfo.SetValue(instance, typedValue);
            }
        }

        private CommandInfo FindCommand(string cmdName)
        {
            for (int i = 0; i < Commands.Length; i++)
            {
                if (Commands[i].Name == cmdName)
                {
                    return Commands[i];
                }
            }
            return null;
        }

        private sealed class Tokens
        {
            private readonly string[] _tokens;
            private int _index;

            public Tokens(string[] args)
            {
                _tokens = args;
                _index = -1;
            }

            public bool IsArgument
            {
                get { return Current.StartsWith('-'); }
            }

            public string Current
            {
                get
                {
                    if (!IsIndexInRange()) { throw new InvalidOperationException(); }

                    return _tokens[_index];
                }
            }

            public bool MoveNext()
            {
                if (_index == -1 && _tokens.Length > 0 ||
                    IsIndexInRange())
                {
                    _index++;
                    return IsIndexInRange();
                }
                return false;
            }

            private bool IsIndexInRange()
            {
                return _index >= 0 && _index < _tokens.Length;
            }
        }


        private class CommandInfo
        {
            public Type Type { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public ArgumentInfo[] Arguments { get; set; }
        }

        private class ArgumentInfo
        {
            public string Property { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
