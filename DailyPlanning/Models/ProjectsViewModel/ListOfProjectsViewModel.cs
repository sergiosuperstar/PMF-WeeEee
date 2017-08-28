using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.ProjectsViewModel
{
    public class ListOfProjectsViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}