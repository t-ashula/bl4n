// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    public abstract class ListTypeCustomField : TypedCustomField
    {
        protected ListTypeCustomField(string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false,
            List<string> items = null, bool? allowInput = null, bool? allowAddItem = null)
            : base(fieldname, applicableIssueTypes, description, required)
        {
            Items = items;
            AllowInput = allowInput;
            AllowAddItem = allowAddItem;
        }

        public List<string> Items { get; private set; }

        public bool? AllowInput { get; set; }

        public bool? AllowAddItem { get; set; }
    }
}