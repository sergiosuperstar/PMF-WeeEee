using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DailyPlanning.Infrastructure.Context
{
    public class DailyPlanningContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }
    }
}