// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Backlog.cs">
// bl4n - Backlog.jp API Client library
// this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N
{
    /// <summary> The backlog. </summary>
    public class Backlog
    {
        /// <summary> API タイプを取得します</summary>
        public APIType APIType
        {
            get { return _settings.APIType; }
        }

        /// <summary> スペース名を取得します </summary>
        public string SpaceName
        {
            get { return _settings.SpaceName; }
        }

        /// <summary> ホスト名を取得します </summary>
        public string HostName
        {
            get { return _settings.HostName; }
        }

        /// <summary> API Key を取得します </summary>
        public string APIKey
        {
            get { return _settings.APIKey; }
        }

        private readonly IBacklogConnectionSettings _settings;

        /// <summary> <see cref="Backlog"/> クラスを初期化します． </summary>
        public Backlog(IBacklogConnectionSettings settings)
        {
            _settings = settings;
        }
    }
}