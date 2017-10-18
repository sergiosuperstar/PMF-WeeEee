using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.TimeSheetsViewModel
{
    public class CompleteTimeSheetViewModel
    {
        public IEnumerable<TimeSheetViewModel> TimeSheets { get; set; }

        public QuickAddTimeSheetViewModel QuickAddTimeSheet { get; set; }

        [DisplayFormat(DataFormatString = "{0:%h}:{0:%m}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"(00|0[0-9]|1[0-9]|2[0-3]):([0-9]|[0-5][0-9]):([0-9]|[0-5][0-9])$", ErrorMessage = "Invalid Time.")]
        public TimeSpan FinishTime { get; set; }

        public int FinishTimeSheetID { get; set; }

    }
}