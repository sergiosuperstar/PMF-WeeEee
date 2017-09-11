using DailyPlanning.Infrastructure.Context;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

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