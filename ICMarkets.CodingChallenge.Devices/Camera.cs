using ICMarkets.CodingChallenge.Devices.Core;
using System;
using System.Collections.Generic;
using System.Text;
using ICMarkets.CodingChallenge.Devices.Connections;
using ICMarkets.CodingChallenge.Devices.ConsoleInterface;

namespace ICMarkets.CodingChallenge.Devices
{
    public class Camera : CoreDevice
    {
        // Not sure if that was meant in a challenge but:
        // Camera (Web) is monopolistic usage device. I mean it can be used by a single thread at a time.
        // But things like Sound card, phones and relevant to our case streaming and IP cameras are read only available to everyone
        // though control is dedicated to a single thread. So
        // If we have 1st type camera we should implement a singleton and lock the whole instance while it is being used
        // But if we have 2nd type then we lock only the control attributes 
        // So as we don't have camera specification I left an abstract camera object

        // ============================== POSSIBLE IMPROVEMENTS ==================================
        // Locking controls for thread safety
        // =======================================================================================

        public double Zoom { get; protected set; } // Zoom LIMITS are acquired via driver, bypassed the step. Let's assume we have it automatically
        public double PosX { get; protected set; } // Same
        public double PosY { get; protected set; } // Same

        public Camera(ConnectionTypes connectionType, object credentials, string alias = null) 
            : base(connectionType, credentials, alias)
        {
            Name = "ICMarketCam v0.1.0";
        }

        [ConsoleCommand("zoom")]
        public void SwitchZoom(double newValue)
        {
            ConsoleOutput($"Changing zoom from {Zoom} to {newValue}", ConsoleColor.DarkGray);
            Zoom = newValue;
            ConsoleOutput("Done", ConsoleColor.DarkGray);
        }

        [ConsoleCommand("pos")]
        public void ChangePosition(double newX, double newY)
        {
            ConsoleOutput($"Changing position from ({PosX}, {PosY}) to ({newX}, {newY})", ConsoleColor.DarkGray);
            PosX = newX;
            PosY = newY;
            ConsoleOutput("Done", ConsoleColor.DarkGray);
        }

        public void Quit()
        {
            Disconnect();
        }
    }
}
