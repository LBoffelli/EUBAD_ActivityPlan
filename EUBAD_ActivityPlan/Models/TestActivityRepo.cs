using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Models
{
    public class TestActivityRepo : IActivityRepo
    {
        public IEnumerable<Activity> AllActivities =>
            new List<Activity>
            {
                new Activity
                {
                    Id = 1,
                    Name = "Coding",
                },
                new Activity
                {
                    Id = 2,
                    Name = "On Call",
                },
                new Activity
                {
                    Id = 3,
                    Name = "Leave",
                },
                new Activity
                {
                    Id = 4,
                    Name = "Gardening",
                },
            };

        public Activity GetActivityById(int activityId)
        {
            return AllActivities.FirstOrDefault(a => a.Id == activityId);
        }

    }
}
