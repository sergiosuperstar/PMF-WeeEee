using System.ComponentModel.DataAnnotations;

namespace DailyPlanning.Models.ProjectsViewModel
{
    public class ProjectViewModel
    {
        [Display(Name = "ID")]
        public int ProjectID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
    }
}