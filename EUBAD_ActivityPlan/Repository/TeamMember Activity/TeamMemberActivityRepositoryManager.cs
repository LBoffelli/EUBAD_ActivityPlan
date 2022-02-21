using EUBAD_ActivityPlan.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Models
{
    public class TeamMemberActivityRepositoryManager : ITeamMemberActivityRepositoryManager
    {
        private readonly AppDbContext _appDbContext;
        public TeamMemberActivityRepositoryManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<TeamMemberActivity>> GetAllTeamActivities()
        {
           return await _appDbContext.TeamMemberActivities.Include(teamMember => teamMember.TeamMember).Include(activity => activity.Activity).Where(teamMemberActivity => teamMemberActivity.TeamMember.IsActive && teamMemberActivity.Activity.IsActive).ToListAsync(); 
        }
        public TeamMemberActivity GetTeamMemberActivityById(int teamMemberActivityId)
        {
            return _appDbContext.TeamMemberActivities.FirstOrDefault(teamMemberActivity => teamMemberActivity.Id == teamMemberActivityId);
        }
        public async Task<IEnumerable<TeamMemberActivity>> GetTeamMemberActivitiesByDate(DateTime startDate, DateTime endDate)
        {
            return (await GetAllTeamActivities()).Where(teamMemberActivity => teamMemberActivity.Day.Date >= startDate.Date && teamMemberActivity.Day.Date <= endDate.Date).OrderBy(teammemberActivity => teammemberActivity.Day);
        }

        public async Task AddTeamMemberActivity(TeamMemberActivity teamMemberActivity)
        {
            _appDbContext.TeamMemberActivities.Add(teamMemberActivity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteTeamMemberActivity(TeamMemberActivity teamMemberActivity)
        {
            if (teamMemberActivity != null)
            {
                _appDbContext.TeamMemberActivities.Remove(teamMemberActivity);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task EditTeamMemberActivity(TeamMemberActivity teamMemberActivity)
        {
            _appDbContext.Update(teamMemberActivity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
