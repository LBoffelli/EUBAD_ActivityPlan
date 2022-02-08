using EUBAD_ActivityPlan.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            //var teamMembersList = new List<SelectListItem>();
            /*foreach (var member in AllMembers)
            {
                if (member.IsActive)
                {
                    teamMembersList.Add(new SelectListItem
                    {
                        Value = member.Id.ToString(),
                        Text = member.FirstName + " " + member.LastName
                    });
                }
            }*/
            var teamMembersList = GetAllMembers().Where(teamMember => teamMember.IsActive).Select(teamMember => new SelectListItem{ 
                Value = teamMember.Id.ToString(), 
                Text = teamMember.FirstName + " " + teamMember.LastName
            });

            return teamMembersList;
        }

        public TeamMember GetTeamMemberById(int teamMemberId)
        {
            return _appDbContext.TeamMembers.FirstOrDefault(teamMember => teamMember.Id == teamMemberId);
        }
        public async Task AddTeamMember(TeamMember teamMember)
        {
            _appDbContext.TeamMembers.Add(teamMember);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteTeamMember(TeamMember teamMember)
        {
            if (teamMember != null)
            {
                _appDbContext.TeamMembers.Remove(teamMember);
                await _appDbContext.SaveChangesAsync();
            }
        }
        public async Task EditTeamMember(TeamMember teamMember)
        {
            _appDbContext.Update(teamMember);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
