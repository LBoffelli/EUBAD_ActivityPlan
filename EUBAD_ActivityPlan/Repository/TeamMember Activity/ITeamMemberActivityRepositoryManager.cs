using EUBAD_ActivityPlan.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Interfaces
{
    public interface ITeamMemberActivityRepositoryManager
    {
        public IEnumerable<TeamMemberActivity> GetAllTeamActivities();
        public TeamMemberActivity GetTeamMemberActivityById(int teamMemberActivityId);

        public IEnumerable<TeamMemberActivity> GetTeamMemberActivitiesByDate(DateTime startDate, DateTime endDate);
        public Task AddTeamMemberActivity(TeamMemberActivity teamMemberActivity);
        public Task DeleteTeamMemberActivity(TeamMemberActivity teamMemberActivity);
        public Task EditTeamMemberActivity(TeamMemberActivity teamMemberActivity);
    }
}
