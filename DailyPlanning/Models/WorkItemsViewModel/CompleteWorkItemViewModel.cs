using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DailyPlanning.Models.PagingModel;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class CompleteWorkItemViewModel
    {
        public IEnumerable<WorkItemViewModel> WorkItems { get; set; }

        public QuickAddWorkItemViewModel QuickAddWorkItem { get; set; }

        public Pager Pager { get; set; }
    }
}