using ICMarkets.CodingChallenge.Devices.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ICMarkets.CodingChallenge.Devices.ConsoleInterface
{
    public static class ConsoleCommandExtensions
    {
        // ============================== POSSIBLE IMPROVEMENTS ==================================
        // For now every time we call the CLI command reflection iterates type members every time
        // We can cache and associate them on the first run to avoid extra job repeating
        // =======================================================================================

        /// <summary>
        /// If source instance contains relevant method and parameters match types then executes it
        /// </summary>
        /// <typeparam name="T">Target instance type</typeparam>
        /// <param name="source">Target instance</param>
        /// <param name="command">CLI command</param>
        public static void ExecuteCommand<T>(this T source, string command) where T : CoreDevice
        {
            var cmdParts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (cmdParts.Length == 0)
            {
                WriteError($"Invalid command {command}");
            }
            var methods = source.GetType().GetMethods()
                .Where(x => x.GetCustomAttribute<ConsoleCommandAttribute>() != null);
            var suitableMethods = methods
                .Where(x => x.GetCustomAttribute<ConsoleCommandAttribute>().Command.Equals(cmdParts[0])
            );

            if (suitableMethods.Count() == 0)
            {
                WriteError("Command not found for this device");
            }
            else if (suitableMethods.Count() > 1)
            {
                WriteError("Ambigious command");
            }
            else
            {
                suitableMethods.FirstOrDefault().InvokeCliCommand(source, cmdParts.Skip(1).ToArray());
            }
        }

        /// <summary>
        /// Executes selected command on target instance
        /// </summary>
        /// <param name="method">Matched method</param>
        /// <param name="source">Target instance</param>
        /// <param name="args">Arguments to pass</param>
        private static void InvokeCliCommand(this MethodInfo method, object source, string[] args)
        {
            var methodArgs = method.GetParameters();
            if (args.Length != methodArgs.Length)
            {
                WriteError($"Method {method.Name} requires {methodArgs.Length} parameters. You provided {args.Length}");
                return;
            }

            List<object> convertedArgs = new List<object>();
            for(int i = 0; i < args.Length; i++)
            {
                try
                {
                    var tryParseMethod = methodArgs[i].ParameterType.GetMethod("Parse", new Type[] { typeof(string) });
                    var val = tryParseMethod.Invoke(source, new object[] { args[i] });
                    convertedArgs.Add(val);
                }
                catch
                {
                    WriteError($"Incompatible type for parameter {methodArgs[i].Name}. Expected {methodArgs[i].ParameterType}");
                    return;
                }
            }
            method.Invoke(source, convertedArgs.ToArray());
        }

        // Just to avoid code duplicates
        private static void WriteError(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(text);
            Console.ResetColor();
        }        
    }
}
