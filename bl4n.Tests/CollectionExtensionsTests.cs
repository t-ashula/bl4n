// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExtensionsTests.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BL4N.Tests
{
    /// <summary> コレクションの拡張子のテストを表します </summary>
    public class CollectionExtensionsTests
    {
        /// <summary> ToKeyValuePairs のテスト </summary>
        [Fact]
        public void ToKeyValuePairsTest()
        {
            var ids = new[] { 1, 2, 3, 4, 5 };
            const string Key = "id[]";
            var expected = new[]
            {
                new KeyValuePair<string, string>(Key, "1"),
                new KeyValuePair<string, string>(Key, "2"),
                new KeyValuePair<string, string>(Key, "3"),
                new KeyValuePair<string, string>(Key, "4"),
                new KeyValuePair<string, string>(Key, "5")
            };
            var actual = ids.ToKeyValuePairs(Key).ToArray();
            Assert.Equal(expected, actual);
        }
    }
}