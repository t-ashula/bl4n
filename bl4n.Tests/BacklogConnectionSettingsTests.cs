// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogConnectionSettingsTests.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace BL4N.Tests
{
    /// <summary> 接続設定に関するテストを表します </summary>
    public class BacklogConnectionSettingsTests
    {
        private const string TestSettings = @"{""APIKey"":""abcdefghijklmn"",""APIType"":""APIKey"",""HostName"":""localhost"",""Port"":34567,""SpaceName"":""example"",""UseSSL"":false}";

        private const string SettingPath = "test.json";

        /// <summary> <see cref="BacklogConnectionSettingsTests"/> のインスタンスを初期化します． </summary>
        public BacklogConnectionSettingsTests()
        {
            if (File.Exists(SettingPath))
            {
                try
                {
                    File.Delete(SettingPath);
                }
                catch
                {
                    // ignore
                }
            }
        }

        /// <summary> ディスクからの読み込みのテスト </summary>
        [Fact]
        public void Load_Path_Test()
        {
            var settings = new BacklogJPConnectionSettings("bl4n", APIType.APIKey, "abcdefghijklmn");
            settings.Save(SettingPath);

            var saved = BacklogConnectionSettings.Load(SettingPath);
            Assert.True(saved.UseSSL);
            Assert.Equal("bl4n.backlog.jp", saved.HostName);
            Assert.Equal("bl4n", saved.SpaceName);
            Assert.Equal(443, saved.Port);
            Assert.Equal(APIType.APIKey, saved.APIType);
            Assert.Equal("abcdefghijklmn", saved.APIKey);
        }

        /// <summary> ストリームからの読み込みのテスト </summary>
        [Fact]
        public void Load_Stream_Test()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(TestSettings)))
            {
                var settings = BacklogConnectionSettings.Load(ms);
                Assert.IsType<BacklogConnectionSettings>(settings);
                Assert.Equal("example", settings.SpaceName);
                Assert.Equal("localhost", settings.HostName);
                Assert.Equal(34567, settings.Port);
                Assert.False(settings.UseSSL);
                Assert.Equal(APIType.APIKey, settings.APIType);
                Assert.Equal("abcdefghijklmn", settings.APIKey);
            }
        }

        /// <summary> Host の取得のテスト </summary>
        [Fact]
        public void HostTest()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(TestSettings)))
            {
                var settings = BacklogConnectionSettings.Load(ms);

                Assert.Equal("localhost", settings.HostName);
                Assert.Equal(34567, settings.Port);
                Assert.False(settings.UseSSL);

                Assert.Equal("localhost:34567", settings.Host);
            }
        }

        /// <summary> ストリームへの書き出しのテスト </summary>
        [Fact]
        public void Save_Stream_Test()
        {
            var settings = new BacklogConnectionSettings(
                apikey: "abcdefghijklmn",
                apiType: APIType.APIKey,
                hostName: "localhost",
                port: 34567,
                spaceName: "example",
                ssl: false);

            using (var ms = new MemoryStream())
            {
                settings.Save(ms);
                var json = Encoding.UTF8.GetString(ms.ToArray());

                Assert.Equal(TestSettings, json);
            }
        }

        /// <summary> ディスクへの保存のテスト </summary>
        [Fact]
        public void Save_Path_Test()
        {
            var settings = new BacklogJPConnectionSettings("bl4n", APIType.APIKey, "abcdefghijklmn");
            settings.Save(SettingPath);
            Assert.True(File.Exists(SettingPath));
        }
    }
}