using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taskmato_2.DTOs.RangeAttributes
{
    public class DateNotInPastAttribute : ValidationAttribute
    {
        public DateNotInPastAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            var t = dt.CompareTo(DateTime.Now);
            return t > 0;
        }
    }
}
