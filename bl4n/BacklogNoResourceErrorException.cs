// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogNoResourceErrorException.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BL4N
{
    /// <summary> If you access the resource that does not exist. </summary>
    public sealed class BacklogNoResourceErrorException : BacklogException
    {
    }
}