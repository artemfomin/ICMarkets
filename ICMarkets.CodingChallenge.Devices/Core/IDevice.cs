using ICMarkets.CodingChallenge.Devices.Connections;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICMarkets.CodingChallenge.Devices.Core
{
    public interface IDevice : IDisposable
    {
        string Name { get; }
        string Alias { get; set; }
        object Type { get; } // E.g.: Sound device, HID device, Camera, Woodoo kettle, [your option]
        string DeviceID { get; } // E.g.: "USB\\00001324" 
        object Driver { get; } // object shoul wire driver version, signature, manufacturer e.t.c.

        bool Connect();
        bool Disconnect();
    }
}
