using EUBAD_ActivityPlan.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Models
{
    public class ActivityRepositoryManager : IActivityRepositoryManager
    {
        private readonly AppDbContext _appDbContext;
        public ActivityRepositoryManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Activity> GetAllActivities() => _appDbContext.Activities;
        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            return await _appDbContext.Activities.ToListAsync();
        }
        public Activity GetActivityById(int activityId)
        {
            return _appDbContext.Activities.FirstOrDefault(a => a.Id == activityId);
        }
        public Task AddActivity(Activity activity)
        {
            _appDbContext.Activities.Add(activity);
            return _appDbContext.SaveChangesAsync();
        }
        public Task DeleteActivity(Activity activity)
        {
            if (activity != null)
            {
                _appDbContext.Activities.Remove(activity);
            }
            return _appDbContext.SaveChangesAsync();
        }
        public Task EditActivity(Activity activity)
        {
            _appDbContext.Update(activity);
            return _appDbContext.SaveChangesAsync();
        }
    }
}

