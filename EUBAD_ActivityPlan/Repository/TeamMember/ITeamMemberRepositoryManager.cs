using EUBAD_ActivityPlan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Interfaces
{
    public interface ITeamMemberRepositoryManager
    {
        public IEnumerable<TeamMember> GetAllMembers();
        public TeamMember GetTeamMemberById(int teamMemberId);
        public Task AddTeamMember(TeamMember teamMember);
        public Task DeleteTeamMember(TeamMember teamMember);
        public Task EditTeamMember(TeamMember teamMember);
    }
}
