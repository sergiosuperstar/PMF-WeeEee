using DailyPlanning.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyPlanning.Infrastructure.Entities
{
    public class WorkItem
    {
        public WorkItem()
        {
        }

        [Key]
        public int WorkItemID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public Status Status { get; set; }
        
        public int ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }
    }
}