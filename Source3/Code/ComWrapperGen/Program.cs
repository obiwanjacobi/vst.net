using System;
using System.Linq;
using System.Reflection;

namespace ComWrapperGen
{
    public static class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
#else
            if (args.Length == 0)
            {
                Console.WriteLine("Specify a path to an Assembly to generate code for.");
                return;
            }
#endif

            var scanner = new Scanner();
            if (args.Length == 0)
                scanner.Scan(Assembly.GetExecutingAssembly());
            else
                scanner.Scan(args[0]);

            if (!scanner.Interfaces.Any())
            {
                Console.WriteLine("No public 'ComInterface' interfaces were found.");
                return;
            }

            var context = new Context
            {
                AbiNamespace = "ABI",
                ComWrapperClassName = "DebugTestComWrappers",
                Namespace = "Test",
            };

            var gen = new CodeGenerator(context);
            foreach (var intf in scanner.Interfaces)
            {
                gen.GenerateInterfaceVtable(intf);
                gen.GenerateABI(intf);
            }
        }
    }
}
