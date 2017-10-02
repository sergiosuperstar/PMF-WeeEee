using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DailyPlanning.Infrastructure.Database
{
    public class DailyPlanningTestDBInitializer : DropCreateDatabaseAlways<DailyPlanningContext>
    {
        private const string SetDBSingleUserMode = "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";

        public override void InitializeDatabase(DailyPlanningContext context)
        {
            if (context.Database.Exists())
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, string.Format(SetDBSingleUserMode, context.Database.Connection.Database));
            }

            base.InitializeDatabase(context);
        }

        protected override void Seed(DailyPlanningContext context)
        {
            var project1 = new Project() { Title = "DailyPlan tracker", IsEnabled = true, IsDeleted = false };
            var project2 = new Project() { Title = "DailyPlan tracker tests", IsEnabled = true, IsDeleted = false };

            context.Projects.Add(project1);
            context.Projects.Add(project2);

            context.SaveChanges();

            var workitem01 = new WorkItem() { Title = "Create new MapperConfiguration class", Description = "Create new MapperConfiguration class for configure all mapping and create mapper, then add it to DI container and inject through constructor to all controllers.", Status = Enums.Status.DONE, ProjectID = project1.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem02 = new WorkItem() { Title = "Investigate HtmlSanitizer", Description = "Install from nugget and see how it works.", Status = Enums.Status.DONE, ProjectID = project1.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem03 = new WorkItem() { Title = "Implement CKEditor for Description in WorkItems", Description = "Investigate what is CKEditor, how to use him on html page and how to bind him to a property of view model.", Status = Enums.Status.DONE, ProjectID = project1.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem04 = new WorkItem() { Title = "Implement string sanitization for Description in WorkItems", Description = "Because Description allow html tags to be inserted. We need a parser who is going to kick out (or replace) potentially dangerous tags.", Status = Enums.Status.IN_PROGRESS, ProjectID = project1.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem05 = new WorkItem() { Title = "Implement Dependency Injection for context", Description = "Inject context through controller constructor.", Status = Enums.Status.IN_PROGRESS, ProjectID = project1.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem06 = new WorkItem() { Title = "Create validation for Add and Update", Description = "Add the same constraint to view models as they are in entities. Add name for every entity. Use DataAnnotation", Status = Enums.Status.TO_DO, ProjectID = project1.ProjectID, IsEnabled = true, IsDeleted = false };

            var workitem07 = new WorkItem() { Title = "In test project add reference to DailyPlan project", Description = "In test project create initialize database method which will call database.initialize from DailyPlan project (need to add reference to DailyPlan from test project).", Status = Enums.Status.DONE, ProjectID = project2.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem08 = new WorkItem() { Title = "Create automated ui tests for entity DailyPlan", Description = "Create coded ui tests for CRU operations regarding DailyPlan.", Status = Enums.Status.TO_DO, ProjectID = project2.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem09 = new WorkItem() { Title = "Create BaseTest class", Description = "In test project create base test that will call initialize database method in test initialize method (add browser window too).", Status = Enums.Status.DONE, ProjectID = project2.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem10 = new WorkItem() { Title = "Add Assert in DetailsDailyPlan test method", Description = "No description provided.", Status = Enums.Status.IN_PROGRESS, ProjectID = project2.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem11 = new WorkItem() { Title = "Create automated ui test for entities Projects and WorkItems", Description = "Create CRUD operation coded ui tests regarding Project and WorkItem. Implement PageObject pattern for pages regarding Projects and WorkItems.", Status = Enums.Status.DONE, ProjectID = project2.ProjectID, IsEnabled = true, IsDeleted = false };
            var workitem12 = new WorkItem() { Title = "Initialize database before each test scenario", Description = "Before call DropAndCreateDatabaseAlways from initializer, close all connection to the database first.", Status = Enums.Status.TO_DO, ProjectID = project2.ProjectID, IsEnabled = true, IsDeleted = false };

            context.WorkItems.Add(workitem01);
            context.WorkItems.Add(workitem02);
            context.WorkItems.Add(workitem03);
            context.WorkItems.Add(workitem04);
            context.WorkItems.Add(workitem05);
            context.WorkItems.Add(workitem06);

            context.WorkItems.Add(workitem07);
            context.WorkItems.Add(workitem08);
            context.WorkItems.Add(workitem09);
            context.WorkItems.Add(workitem10);
            context.WorkItems.Add(workitem11);
            context.WorkItems.Add(workitem12);

            context.SaveChanges();

            var witems1 = new List<WorkItem>();
            var witems2 = new List<WorkItem>();
            var witems3 = new List<WorkItem>();
            var witems4 = new List<WorkItem>();
            var witems5 = new List<WorkItem>();
            var witems6 = new List<WorkItem>();
            var witems7 = new List<WorkItem>();
            var witems8 = new List<WorkItem>();
            var witems9 = new List<WorkItem>();
            var witems10 = new List<WorkItem>();
            var witems11 = new List<WorkItem>();
            var witems12 = new List<WorkItem>();
            var witems13 = new List<WorkItem>();
            var witems14 = new List<WorkItem>();
            var witems15 = new List<WorkItem>();
            var witems16 = new List<WorkItem>();

            witems1.Add(workitem12);
            witems2.Add(workitem12);
            witems2.Add(workitem10);

            witems3.Add(workitem12);
            witems3.Add(workitem10);
            witems4.Add(workitem09);
            witems4.Add(workitem07);

            witems5.Add(workitem09);
            witems5.Add(workitem07);
            witems6.Add(workitem11);
            witems6.Add(workitem08);
            witems6.Add(workitem09);

            witems7.Add(workitem11);
            witems7.Add(workitem08);
            witems7.Add(workitem09);
            witems8.Add(workitem06);

            witems9.Add(workitem06);
            witems10.Add(workitem05);

            witems11.Add(workitem05);
            witems12.Add(workitem02);
            witems12.Add(workitem03);
            witems12.Add(workitem04);

            witems13.Add(workitem04);
            witems13.Add(workitem03);
            witems13.Add(workitem02);
            witems14.Add(workitem01);

            witems15.Add(workitem01);
            witems16.Add(workitem01);


            var DailyPlan1 = new DailyPlan() { Date = DateTime.Now.AddDays(-1), Today = witems1, DayBefore = witems2, Note = "Resolving issues with initializing database before each test" };
            var DailyPlan2 = new DailyPlan() { Date = DateTime.Now.AddDays(-2), Today = witems3, DayBefore = witems4, Note = "Initializing database before each test and fixing some issues in tests " };
            var DailyPlan3 = new DailyPlan() { Date = DateTime.Now.AddDays(-3), Today = witems5, DayBefore = witems6, Note = "Adding reference in DailyPlan project and creating BaseTest class" };
            var DailyPlan4 = new DailyPlan() { Date = DateTime.Now.AddDays(-4), Today = witems7, DayBefore = witems8, Note = "Creating automated ui tests for WorkItem, Project and DailyPlan" };
            var DailyPlan5 = new DailyPlan() { Date = DateTime.Now.AddDays(-5), Today = witems9, DayBefore = witems10, Note = "Creating Validation for Add and Update" };
            var DailyPlan6 = new DailyPlan() { Date = DateTime.Now.AddDays(-6), Today = witems11, DayBefore = witems12, Note = "Implementation Dependency Injection and fixing description for WorkItems" };
            var DailyPlan7 = new DailyPlan() { Date = DateTime.Now.AddDays(-7), Today = witems13, DayBefore = witems14, Note = "Implementation description with CKEditor and string sanitization" };
            var DailyPlan8 = new DailyPlan() { Date = DateTime.Now.AddDays(-8), Today = witems15, DayBefore = witems16, Note = "Creating MapperConfiguration" };


            context.DailyPlans.Add(DailyPlan1);
            context.DailyPlans.Add(DailyPlan2);
            context.DailyPlans.Add(DailyPlan3);
            context.DailyPlans.Add(DailyPlan4);
            context.DailyPlans.Add(DailyPlan5);
            context.DailyPlans.Add(DailyPlan6);
            context.DailyPlans.Add(DailyPlan7);
            context.DailyPlans.Add(DailyPlan8);

            base.Seed(context);
        }
    }
}