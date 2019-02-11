using System;
using System.Collections.Generic;
using System.Text;

namespace ICMarkets.CodingChallenge.Devices.Connections
{
    /// <summary>
    /// Device connection details
    /// </summary>
    public interface IConnection
    {
        #region Properties
        bool IsActive { get; }
        ConnectionTypes Type { get; set; }
        object Credentials { get; set; } // Whatever here - connection strings, IPs, e.t.c. Should be a typed object

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Attach to device via selected transport protocol and allocate requred resources
        /// </summary>
        /// <returns>True if successfull</returns>
        bool Connect();

        /// <summary>
        /// Detach from devica and release resources
        /// </summary>
        /// <returns></returns>
        bool Disconnect();

        #endregion Public methods
    }
}
