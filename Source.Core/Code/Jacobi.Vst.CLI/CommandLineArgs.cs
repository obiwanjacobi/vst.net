using System;

namespace Jacobi.Vst.CLI
{
    internal sealed class CommandLineArgs
    {
        private static readonly CommandInfo[] Commands = new[]
        {
            new CommandInfo { Name = "publish", Description="Gathers all dependencies based on a root .deps.json file.",
                Arguments = new[] {
                    new ArgumentInfo { Name = "-o", Description="The directory that will receive all the files." },
                    new ArgumentInfo { Name = "-d", Description="The .deps.json file to publish." }
                }
            }
        };

        public CommandLineArgs(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                Parse(args);
            }
        }

        public ICommand Command { get; private set; }

        private void Parse(string[] args)
        {
            var tokens = new Tokens(args);

            while (tokens.MoveNext())
            {
                if (tokens.IsCommand)
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
            // TODO:
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

            public bool IsCommand
            {
                get { return !Current.StartsWith('-'); }
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
                if (IsIndexInRange())
                {
                    _index++;
                    return true;
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
            public string Name { get; set; }
            public string Description { get; set; }
            public ArgumentInfo[] Arguments { get; set; }
        }

        private class ArgumentInfo
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
