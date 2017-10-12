using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.TimeSheetsViewModel
{
    public class CompleteTimeSheetViewModel
    {
        public IEnumerable<TimeSheetViewModel> TimeSheets { get; set; }

        public QuickAddTimeSheetViewModel QuickAddTimeSheet { get; set; }

    }
}