// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtraJsonPropertyReadableObjectTests.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;
using BL4N.Data;
using Xunit;

namespace BL4N.Tests
{
    /// <summary>
    /// tests for <see cref="ExtraJsonPropertyReadableObject"/>
    /// </summary>
    public class ExtraJsonPropertyReadableObjectTests
    {
        private const string ExtraJsonString = @"{ ""key1"" : ""value1"", ""extra1"" : ""extra value"" }";

        [DataContract]
        public class SomeClass : ExtraJsonPropertyReadableObject
        {
            [DataMember(Name = "key1")]
            public string Key1 { get; set; }
        }

        /// <summary>
        /// test for <see cref="ExtraJsonPropertyReadableObject.HasExtraProperty"></see>
        /// </summary>
        [Fact]
        public void HasExtraPropertyTest()
        {
            var some = Backlog.DeserializeObj<SomeClass>(ExtraJsonString);
            Assert.True(some.HasExtraProperty());
        }

        /// <summary>
        /// test for <see cref="ExtraJsonPropertyReadableObject.GetExtraProperties"></see>
        /// </summary>
        [Fact]
        public void GetExtraPropsTest()
        {
            var some = Backlog.DeserializeObj<SomeClass>(ExtraJsonString);
            var props = some.GetExtraProperties();
            Assert.Contains("extra1", props.Keys);
            Assert.Equal("extra value", props["extra1"]);
        }
    }
}