using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DailyPlanning.Infrastructure.Database
{
    public class DailyPlanningDBInitializer : DropCreateDatabaseAlways<DailyPlanningContext>
    {
        protected override void Seed(DailyPlanningContext context)
        {
            var project1 = new Project() { Title = "Project 1" };
            var project2 = new Project() { Title = "Project 2" };
            var project3 = new Project() { Title = "Project 3" };
            
            context.Projects.Add(project1);
            context.Projects.Add(project2);
            context.Projects.Add(project3);

            context.SaveChanges();

            var workitem1 = new WorkItem() { Title = "WorkItem 1", Description = "First WorkItem", Status = Enums.Status.TODO, ProjectID = project1.ProjectID };
            var workitem2 = new WorkItem() { Title = "WorkItem 2", Description = "Second WorkItem", Status = Enums.Status.IN_PROGRESS, ProjectID = project2.ProjectID };
            var workitem3 = new WorkItem() { Title = "WorkItem 3", Description = "Third WorkItem", Status = Enums.Status.DONE, ProjectID = project3.ProjectID };

            context.WorkItems.Add(workitem1);
            context.WorkItems.Add(workitem2);
            context.WorkItems.Add(workitem3);

            context.SaveChanges();

            base.Seed(context);
        }

    }
}