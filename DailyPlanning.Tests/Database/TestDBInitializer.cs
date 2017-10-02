using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanning.Tests.Database
{
    public static class TestDBInitializer
    {
        public static void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new DailyPlanningTestDBInitializer());
            using (var db = new DailyPlanningContext())
            {
                db.Database.Initialize(true);
            }
        }
    }
}
