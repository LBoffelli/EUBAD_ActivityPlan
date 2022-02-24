using EUBAD_ActivityPlan.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Models
{
    public class TeamMemberRepositoryManager : ITeamMemberRepositoryManager
    {
        private readonly AppDbContext _appDbContext;
        public TeamMemberRepositoryManager(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<TeamMember> GetAllMembers() => _appDbContext.TeamMembers;
        public async Task<IEnumerable<TeamMember>> GetMembersAsync()
        {
            return await _appDbContext.TeamMembers.ToListAsync();
        }
        public TeamMember GetTeamMemberById(int teamMemberId)
        {
            return _appDbContext.TeamMembers.FirstOrDefault(teamMember => teamMember.Id == teamMemberId);
        }
        public Task AddTeamMember(TeamMember teamMember)
        {
            _appDbContext.TeamMembers.Add(teamMember);
            return _appDbContext.SaveChangesAsync();
        }
        public Task DeleteTeamMember(TeamMember teamMember)
        {
            if (teamMember != null)
            {
                _appDbContext.TeamMembers.Remove(teamMember);
            }
            return _appDbContext.SaveChangesAsync();
        }
        public Task EditTeamMember(TeamMember teamMember)
        {
            _appDbContext.Update(teamMember);
            return _appDbContext.SaveChangesAsync();
        }
    }
}
