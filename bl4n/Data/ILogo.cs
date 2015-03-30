// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogo.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BL4N.Data
{
    public interface ILogo
    {
        string FileName { get; }

        Image Content { get; }
    }

    internal sealed class Logo : ILogo
    {
        public string FileName { get; private set; }

        public Image Content { get; private set; }

        public Logo(string file, byte[] content)
        {
            FileName = file;
            using (var ms = new MemoryStream(content))
            {
                Content = new Bitmap(ms);
            }
        }
    }
}