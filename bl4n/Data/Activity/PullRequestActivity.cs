// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PullRequestActivity.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    [DataContract]
    internal sealed class PullRequestActivity : Activity
    {
        [DataMember(Name = "content")]
        private PullRequestActivityContent _content;

        public override IActivityContent Content
        {
            get { return _content; }
        }
    }
}