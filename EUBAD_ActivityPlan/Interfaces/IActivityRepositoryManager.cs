﻿using EUBAD_ActivityPlan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Interfaces
{
    public interface IActivityRepositoryManager
    {
        public IEnumerable<Activity> GetAllActivities();
        public Activity GetActivityById(int activityId);
        public IEnumerable<SelectListItem> GetSelectListItems();
        public Task AddActivity(Activity activity);
        public Task DeleteActivity(Activity activity);
        public Task EditActivity(Activity activity);
    }
}
