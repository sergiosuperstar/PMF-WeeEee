using AutoMapper;
using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.DailyPlansViewModel;
using DailyPlanning.Models.ProjectsViewModel;
using DailyPlanning.Models.WorkItemsViewModel;

namespace DailyPlanning.MapperConfig
{
    public static class MapperProvider
    {
        public static MapperConfiguration Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectViewModel>();
                cfg.CreateMap<AddProjectViewModel, Project>();
                cfg.CreateMap<Project, UpdateProjectViewModel>();
                cfg.CreateMap<UpdateProjectViewModel, Project>();

                cfg.CreateMap<WorkItem, WorkItemViewModel>();
                cfg.CreateMap<WorkItem, QuickAddWorkItemViewModel>();
                cfg.CreateMap<WorkItemViewModel, WorkItem>();
                cfg.CreateMap<QuickAddWorkItemViewModel, WorkItem>();
                cfg.CreateMap<AddWorkItemViewModel, WorkItem>();
                cfg.CreateMap<WorkItem, UpdateWorkItemViewModel>();
                cfg.CreateMap<UpdateWorkItemViewModel, WorkItem>();

                cfg.CreateMap<DailyPlan, DailyPlanViewModel>();
                cfg.CreateMap<AddDailyPlanViewModel, DailyPlan>();
                cfg.CreateMap<DailyPlan, UpdateDailyPlanViewModel>();
                cfg.CreateMap<DailyPlan, DetailsDailyPlanViewModel>();
                cfg.CreateMap<UpdateDailyPlanViewModel, DailyPlan>();
            });

            return config;
        }
    }
}