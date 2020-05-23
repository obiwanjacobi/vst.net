namespace Jacobi.Vst.CLI
{
    internal sealed class HelpCommand : ICommand
    {
        public bool Execute()
        {
            CommandLineArgs.Help();
            return true;
        }
    }
}
