using EUBAD_ActivityPlan.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
        public Activity GetActivityById(int activityId)
        {
            return _appDbContext.Activities.FirstOrDefault(a => a.Id == activityId);
        }
        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            /*var actList = new List<SelectListItem>();
            foreach (var activity in GetAllActivities())
            {
                if (activity.IsActive)
                {
                    actList.Add(new SelectListItem
                    {
                        Value = activity.Id.ToString(),
                        Text = activity.Name
                    });
                }
            }*/
            var activityList = GetAllActivities().Where(activity => activity.IsActive).Select(activity => new SelectListItem
            {
                Value = activity.Id.ToString(),
                Text = activity.Name
            });
            return activityList;
        }
        public async Task AddActivity(Activity activity)
        {
            _appDbContext.Activities.Add(activity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteActivity(Activity activity)
        {
            if (activity != null)
            {
                _appDbContext.Activities.Remove(activity);
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task EditActivity(Activity activity)
        {
            _appDbContext.Update(activity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

