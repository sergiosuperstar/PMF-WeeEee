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
            IList<Project> defaultProjects = new List<Project>();

            defaultProjects.Add(new Project() { Title = "Project 1" });
            defaultProjects.Add(new Project() { Title = "Project 2" });
            defaultProjects.Add(new Project() { Title = "Project 3" });

            foreach (Project p in defaultProjects)
                context.Projects.Add(p);

            IList<WorkItem> defaultWorkitems = new List<WorkItem>();

            defaultWorkitems.Add(new WorkItem() { Title = "WorkItem 1", Description = "First WorkItem", Status = Enums.Status.TODO, ProjectID = 5 });
            defaultWorkitems.Add(new WorkItem() { Title = "WorkItem 2", Description = "Second WorkItem", Status = Enums.Status.IN_PROGRESS, ProjectID = 6 });
            defaultWorkitems.Add(new WorkItem() { Title = "WorkItem 3", Description = "Third WorkItem", Status = Enums.Status.DONE, ProjectID = 7 });

            foreach (WorkItem witem in defaultWorkitems)
                context.WorkItems.Add(witem);

            IList<DailyPlan> defaultDailyPlan = new List<DailyPlan>();

            defaultDailyPlan.Add(new DailyPlan() { Date = DateTime.Now, Today = defaultWorkitems, DayBefore = defaultWorkitems, Note = "Daily Plan 1"});
            defaultDailyPlan.Add(new DailyPlan() { Date = DateTime.Now, Today = defaultWorkitems, DayBefore = defaultWorkitems, Note = "Daily Plan 2"});
            defaultDailyPlan.Add(new DailyPlan() { Date = DateTime.Now, Today = defaultWorkitems, DayBefore = defaultWorkitems, Note = "Daily Plan 3"});

            foreach (DailyPlan dplan in defaultDailyPlan)
                context.DailyPlans.Add(dplan);

            base.Seed(context);
        }

    }
}