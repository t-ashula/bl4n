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
    /// <summary> xUnit でのテスト用の NancyHost を表します </summary>
    public class BacklogMockServerFixture : IDisposable
    {
        private readonly NancyHost _mockServer;

        /// <summary> <see cref="BacklogMockServerFixture"/> のインスタンスを初期化します． </summary>
        public BacklogMockServerFixture()
        {
            _mockServer = new NancyHost(new Uri("http://localhost:34567/"));
            _mockServer.Start();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_mockServer != null)
            {
                _mockServer.Stop();
                _mockServer.Dispose();
            }
        }

        /// <summary> テスト用サーバを取得します． </summary>
        public NancyHost MockServer
        {
            get { return _mockServer; }
        }
    }
}