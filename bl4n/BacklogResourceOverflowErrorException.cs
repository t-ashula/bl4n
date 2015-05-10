// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogResourceOverflowErrorException.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BL4N
{
    /// <summary> If you call API to add a resource when it exceeds a limit provided in the resource. </summary>
    public sealed class BacklogResourceOverflowErrorException : BacklogException
    {
    }
}