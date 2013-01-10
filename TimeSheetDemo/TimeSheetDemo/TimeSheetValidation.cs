using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using TimeSheetControl;

namespace TimeSheetDemo
{
    public class TimeSheetRecordValidation : AbstractValidator<TimeSheetRecord>
    {
        public TimeSheetRecordValidation()
        {
            RuleFor(ts => ts.TimeSheetType).NotEmpty();
            RuleFor(ts => ts.FromTime).NotEmpty();
            RuleFor(ts => ts.ToTime).GreaterThan(ts => ts.FromTime);
        }
    }
}
