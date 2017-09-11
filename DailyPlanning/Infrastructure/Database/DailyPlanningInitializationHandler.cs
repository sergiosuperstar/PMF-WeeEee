using DailyPlanning.Infrastructure.Context;

namespace DailyPlanning.Infrastructure.Database
{
    public static class DailyPlanningInitializationHandler
    {
        public static void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new DailyPlanningDBInitializer());
            using (var db = new DailyPlanningContext())
            {
                db.Database.Initialize(false);
            }
        }
    }
}