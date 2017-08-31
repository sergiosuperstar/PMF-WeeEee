using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.ProjectsViewModel
{
    public class ProjectViewModel
    {
        [Display(Name = "ID")]
        public int ProjectID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}