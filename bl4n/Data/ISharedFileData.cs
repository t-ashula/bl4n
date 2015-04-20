// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISharedFileData.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    public interface ISharedFileData
    {
        string FileName { get; }

        byte[] Content { get; }
    }

    internal class SharedFileData : ISharedFileData
    {
        public SharedFileData(string fileName, byte[] content)
        {
            FileName = fileName;
            Content = content;
        }

        public string FileName { get; private set; }

        public byte[] Content { get; private set; }
    }
}