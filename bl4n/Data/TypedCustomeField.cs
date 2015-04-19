// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypedCustomeField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    public abstract class TypedCustomeField
    {
        public abstract CustomFieldTypes TypeId { get; }

        public string Name { get; set; }

        public long[] ApplicableIssueTypes { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        protected TypedCustomeField(string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false)
        {
            Name = fieldname;
            ApplicableIssueTypes = applicableIssueTypes;
            Description = description;
            Required = required;
        }
    }
}