using System.ComponentModel.DataAnnotations;

namespace DailyPlanning.Infrastructure.Entities
{
    public class Project
    {
        public Project()
        {
        }

        [Key]
        public int ProjectID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsDeleted { get; set; }
    }
}