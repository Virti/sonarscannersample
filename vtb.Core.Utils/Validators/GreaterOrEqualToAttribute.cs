using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace vtb.Core.Utils.Validators
{
    public class GreaterOrEqualToAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public GreaterOrEqualToAttribute(int minValue)
        {
            _minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            return ((int)value) >= _minValue;
        }
    }
}
