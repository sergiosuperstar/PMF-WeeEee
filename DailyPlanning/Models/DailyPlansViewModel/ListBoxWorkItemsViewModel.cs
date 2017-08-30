using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class ListBoxWorkItemsViewModel
    {

        public IEnumerable<string> SelectedWorkItems { get; set; }
        public IEnumerable<SelectListItem> WorkItems { get; set; }
    }
}