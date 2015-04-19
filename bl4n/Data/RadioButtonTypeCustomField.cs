// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioButtonTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    public class RadioButtonTypeCustomField : ListTypeCustomField
    {
        public RadioButtonTypeCustomField(string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false,
            List<string> items = null, bool? allowInput = null, bool? allowAddItem = null)
            : base(fieldname, applicableIssueTypes, description, required, items, allowInput, allowAddItem)
        {
        }

        public override CustomFieldTypes TypeId
        {
            get { return CustomFieldTypes.Radio; }
        }
    }
}