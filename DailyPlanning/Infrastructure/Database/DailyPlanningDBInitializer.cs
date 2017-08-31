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
            var project1 = new Project() { Title = "Project 1", IsEnabled = true, IsDeleted = false };
            var project2 = new Project() { Title = "Project 2", IsEnabled = true, IsDeleted = false };
            var project3 = new Project() { Title = "Project 3", IsEnabled = true, IsDeleted = false };
            
            context.Projects.Add(project1);
            context.Projects.Add(project2);
            context.Projects.Add(project3);

            context.SaveChanges();

            var workitem1 = new WorkItem() { Title = "WorkItem 1", Description = "First WorkItem", Status = Enums.Status.TODO, ProjectID = project1.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem2 = new WorkItem() { Title = "WorkItem 2", Description = "Second WorkItem", Status = Enums.Status.IN_PROGRESS, ProjectID = project2.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem3 = new WorkItem() { Title = "WorkItem 3", Description = "Third WorkItem", Status = Enums.Status.DONE, ProjectID = project3.ProjectID, IsEnabled = true, IsDeleted = false };

            context.WorkItems.Add(workitem1);
            context.WorkItems.Add(workitem2);
            context.WorkItems.Add(workitem3);

            context.SaveChanges();

            var witems1 = new List<WorkItem>();
            var witems2 = new List<WorkItem>();

            witems1.Add(workitem1);
            witems1.Add(workitem2);
            witems2.Add(workitem2);
            witems2.Add(workitem3);

            var DailyPlan1 = new DailyPlan() { Date = DateTime.Now, Today = witems1, DayBefore = witems2, Note = "Daily Plan 1" };
            var DailyPlan2 = new DailyPlan() { Date = DateTime.Now, Today = witems1, DayBefore = witems2, Note = "Daily Plan 2" };
            var DailyPlan3 = new DailyPlan() { Date = DateTime.Now, Today = witems1, DayBefore = witems2, Note = "Daily Plan 3" };

            context.DailyPlans.Add(DailyPlan1);
            context.DailyPlans.Add(DailyPlan2);
            context.DailyPlans.Add(DailyPlan3);

            base.Seed(context);
        }

    }
}