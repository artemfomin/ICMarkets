using System;
using System.Collections.Generic;
using System.Text;

namespace ICMarkets.CodingChallenge.Devices.ConsoleInterface
{
    public class ConsoleCommandAttribute : Attribute
    {
        internal string Command { get; set; }

        public ConsoleCommandAttribute(string command)
        {
            Command = command;
        }
    }
}
