using System;
using System.Collections.Generic;
using System.Text;

namespace vtb.Core.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string GetValueOrDefault(this string val, string defaultVal)
            => !string.IsNullOrEmpty(val) ? val : defaultVal;

        private const string DESC = "DESC";
        private const string ASC = "ASC";

        public static string ParseOrderStringForDynamicLinq(this string val, string defaultVal)
        {
            val = val.GetValueOrDefault(defaultVal);

            var direction = ASC;
            var colName = val;

            if(val[0] == '-')
            {
                direction = DESC;
                colName = val.Substring(1);
            }

            return $"{colName} {direction}";
        }
    }
}
