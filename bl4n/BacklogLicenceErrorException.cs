// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BacklogLicenceErrorException.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace BL4N
{
    /// <summary> If you call the API that is not available in your licence. </summary>
    public sealed class BacklogLicenceErrorException : BacklogException
    {
    }
}