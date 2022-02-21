using EUBAD_ActivityPlan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Interfaces
{
    public interface IActivityRepositoryManager
    {
        public IEnumerable<Activity> GetAllActivities();
        public Task<IEnumerable<Activity>> GetActivitiesAsync();
        public Activity GetActivityById(int activityId);
        public Task AddActivity(Activity activity);
        public Task DeleteActivity(Activity activity);
        public Task EditActivity(Activity activity);
    }
}
