// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SVNRepositoryActivity.cs">
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
    internal sealed class SVNRepositoryActivity : Activity
    {
        [DataMember(Name = "content")]
        private SVNRepositoryActivityContent _content;

        public override IActivityContent Content
        {
            get { return _content; }
        }
    }
}