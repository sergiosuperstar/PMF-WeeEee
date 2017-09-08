using System.Collections.Generic;

namespace DailyPlanning.Models.ProjectsViewModel
{
    public class ListOfProjectsViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}