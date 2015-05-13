// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueTypeColor.cs" company="">
//   bl4n - Backlog.jp API Client library
//   //   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> 課題種別の色を表します </summary>
    public sealed class IssueTypeColor
    {
        private readonly string _colorCode;
        private readonly Color _color;

        /// <summary> <see cref="IssueTypeColor"/> のインスタンスを初期化します </summary>
        /// <param name="colorCode">カラーコード </param>
        private IssueTypeColor(string colorCode)
        {
            _colorCode = colorCode;
            var v = colorCode.Substring(1);
            var r = Convert.ToInt32(v.Substring(0, 2), 16);
            var g = Convert.ToInt32(v.Substring(2, 2), 16);
            var b = Convert.ToInt32(v.Substring(4, 2), 16);
            _color = Color.FromArgb(r, g, b);
        }

        /// <summary>カラーコードを取得します </summary>
        public string ColorCode
        {
            get { return _colorCode; }
        }

        /// <summary> 色を取得します </summary>
        public Color Color
        {
            get { return _color; }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return ColorCode;
        }

        /// <summary> Color1 </summary>
        public static readonly IssueTypeColor Color1 = new IssueTypeColor("#e30000");

        /// <summary> Color2 </summary>
        public static readonly IssueTypeColor Color2 = new IssueTypeColor("#990000");

        /// <summary> Color3 </summary>
        public static readonly IssueTypeColor Color3 = new IssueTypeColor("#934981");

        /// <summary> Color4 </summary>
        public static readonly IssueTypeColor Color4 = new IssueTypeColor("#814fbc");

        /// <summary> Color5 </summary>
        public static readonly IssueTypeColor Color5 = new IssueTypeColor("#2779ca");

        /// <summary> Color6 </summary>
        public static readonly IssueTypeColor Color6 = new IssueTypeColor("#007e9a");

        /// <summary> Color7 </summary>
        public static readonly IssueTypeColor Color7 = new IssueTypeColor("#7ea800");

        /// <summary> Color8 </summary>
        public static readonly IssueTypeColor Color8 = new IssueTypeColor("#ff9200");

        /// <summary> Color9 </summary>
        public static readonly IssueTypeColor Color9 = new IssueTypeColor("#ff3265");

        /// <summary> Color10 </summary>
        public static readonly IssueTypeColor Color10 = new IssueTypeColor("#666665");
    }
}