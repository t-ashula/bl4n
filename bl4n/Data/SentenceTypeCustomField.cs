// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SentenceTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    public class SentenceTypeCustomField : TypedCustomeField
    {
        public SentenceTypeCustomField(string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false)
            : base(fieldname, applicableIssueTypes, description, required)
        {
        }

        public override CustomFieldTypes TypeId
        {
            get { return CustomFieldTypes.Sentence; }
        }
    }
}