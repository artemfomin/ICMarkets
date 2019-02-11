using System;
using System.Collections.Generic;
using System.Text;

namespace ICMarkets.CodingChallenge.Devices.Connections
{
    public class Connection : IConnection
    {
        private bool _isActive = false;

        public bool IsActive { get { return _isActive; } }
        public ConnectionTypes Type { get; set; }
        public object Credentials { get; set; }

        public bool Connect()
        {
            _isActive = true;
            return true;
            // In real life we have to handle possible errors
            // but yet in the task we have a connection only as abstraction
            // we can neglect the details
        }

        public bool Disconnect()
        {
            _isActive = false;
            return true;
        }
    }
}
