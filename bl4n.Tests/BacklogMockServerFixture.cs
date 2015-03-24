// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogMockServerFixture.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Nancy.Hosting.Self;

namespace BL4N.Tests
{
    public class BacklogMockServerFixture : IDisposable
    {
        private readonly NancyHost _mockServer;

        public BacklogMockServerFixture()
        {
            _mockServer = new NancyHost(new Uri("http://localhost:34567/"));
            _mockServer.Start();
        }

        public void Dispose()
        {
            if (_mockServer != null)
            {
                _mockServer.Stop();
                _mockServer.Dispose();
            }
        }

        public NancyHost MockServer
        {
            get { return _mockServer; }
        }
    }
}