using System;
using System.Collections.Generic;
using System.Text;

namespace ICMarkets.CodingChallenge.Devices.Connections
{
    /// <summary>
    /// Formation of possible connection types
    /// </summary>
    public enum ConnectionTypes
    {
        TCP = 1,
        HTTP = 2,
        WebSocket = 3,
        SerialPort = 4,
        USB = 5
        // Whatever else here
    }
}
