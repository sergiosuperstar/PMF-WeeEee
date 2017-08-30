using DailyPlanning.Infrastructure.Database;
using DailyPlanning.Infrastructure.Entities;
using System.Data.Entity;

namespace DailyPlanning.Infrastructure.Context
{
    public class DailyPlanningContext : DbContext
    {

        public DailyPlanningContext() : base()
        {
            System.Data.Entity.Database.SetInitializer(new DailyPlanningDBInitializer());
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        public DbSet<DailyPlan> DailyPlans { get; set; }
       
    }
}