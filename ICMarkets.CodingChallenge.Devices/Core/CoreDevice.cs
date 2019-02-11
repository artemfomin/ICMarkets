using ICMarkets.CodingChallenge.Devices.Connections;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICMarkets.CodingChallenge.Devices.Core
{
    public abstract class CoreDevice : IDevice
    {
        protected IConnection Connection { get; set; }
        public string Name { get; internal set; }
        public string Alias { get; set; }

        protected string AliasInline
        {
            get { return string.IsNullOrWhiteSpace(Alias) ? string.Empty : $"({Alias})"; }
        }

        public object Type { get; internal set; }
        public string DeviceID { get; internal set; }
        public object Driver { get; internal set; }

        public CoreDevice(ConnectionTypes connectionType, object credentials, string alias = null)
        {
            Connection = new Connection
            {
                Type = connectionType,
                Credentials = credentials
            };
            Alias = alias;
        }

        public bool Connect()
        {
            if (Connection.IsActive)
            {
                ConsoleOutput($"Device {Name}{AliasInline} is already connected on {Connection.Type.ToString()}", ConsoleColor.DarkYellow);
                return false;
            }

            if (Connection.Connect())
            {
                ConsoleOutput($"Device {Name}{AliasInline} connected on {Connection.Type.ToString()}", ConsoleColor.DarkCyan);
                return true;
            }
            else
            {
                ConsoleOutput($"Error connecting device {Name}{AliasInline} via {Connection.Type.ToString()}", ConsoleColor.DarkRed);
                return false;
            }
        }

        public bool Disconnect()
        {
            if (!Connection.IsActive)
            {
                ConsoleOutput($"Device {Name}{AliasInline} is already disconnected", ConsoleColor.DarkYellow);
                return false;
            }

            if (Connection.Disconnect())
            {
                ConsoleOutput($"Device {Name}{AliasInline} disconnected from {Connection.Type.ToString()}", ConsoleColor.DarkYellow);
                return true;
            }
            else
            {
                ConsoleOutput($"Error disconnecting device {Name}{AliasInline} from {Connection.Type.ToString()}", ConsoleColor.DarkRed);
                return false;
            }
        }

        public void Dispose()
        {
            Disconnect();
        }

        protected void ConsoleOutput(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
