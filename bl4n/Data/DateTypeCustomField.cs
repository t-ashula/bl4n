// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    public class DateTypeCustomField : TypedCustomField
    {
        public DateTypeCustomField(string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false,
            string firstDate = null, string lastDate = null, int? initialValueType = null, string initialDate = null, int? initialShift = null)
            : base(fieldname, applicableIssueTypes, description, required)
        {
            FirstDate = firstDate;
            LastDate = lastDate;
            InitialValueType = initialValueType;
            InitialDate = initialDate;
            InitailShift = initialShift;
        }

        public override CustomFieldTypes TypeId
        {
            get { return CustomFieldTypes.Date; }
        }

        public string FirstDate { get; set; }

        public string LastDate { get; set; }

        public int? InitialValueType { get; set; }

        public string InitialDate { get; set; }

        public int? InitailShift { get; set; }
    }
}