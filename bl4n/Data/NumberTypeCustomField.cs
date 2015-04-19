// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberTypeCustomField.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace BL4N.Data
{
    public class NumberTypeCustomField : TypedCustomeField
    {
        public NumberTypeCustomField(string fieldname, long[] applicableIssueTypes = null, string description = null, bool required = false,
            double? minValue = null, double? maxValue = null, double? initialValue = null, string unit = null)
            : base(fieldname, applicableIssueTypes, description, required)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            InitailValue = initialValue;
            Unit = unit;
        }

        public override CustomFieldTypes TypeId
        {
            get { return CustomFieldTypes.Number; }
        }

        public double? MinValue { get; set; }

        public double? MaxValue { get; set; }

        public double? InitailValue { get; set; }

        public string Unit { get; set; }
    }
}