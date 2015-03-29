// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectActivity.cs">
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
    internal sealed class ProjectActivity : Activity
    {
        [DataMember(Name = "content")]
        private ProjectActivityContent _content;

        [IgnoreDataMember]
        public override IActivityContent Content
        {
            get { return _content; }
        }
    }
}