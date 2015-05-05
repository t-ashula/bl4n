// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAttributeInfo.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BL4N.Data
{
    /// <summary> 属性情報を表します </summary>
    public interface IAttributeInfo
    {
    }

    [DataContract]
    internal class AttributeInfo : IAttributeInfo
    {
    }
}