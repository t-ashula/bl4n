// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IActivityContent.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> activity's content base type </summary>
    public interface IActivityContent
    {
    }

    [DataContract]
    internal abstract class ActivityContent : IActivityContent
    {
    }
}