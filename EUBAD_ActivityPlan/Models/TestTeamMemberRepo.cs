using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Models
{
    public class TestTeamMemberRepo : ITeamMemberRepo
    {
        public IEnumerable<TeamMember> AllMembers =>
            new List<TeamMember>
            {
                new TeamMember
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@qvc.com"
                },
                new TeamMember
                {
                    Id = 2,
                    FirstName = "Franco",
                    LastName = "Rossi",
                    Email = "fr.123@qvc.com"
                },
                new TeamMember
                {
                    Id = 3,
                    FirstName = "Gray",
                    LastName = "Hound",
                    Email = "imadog@bark.com"
                },
                new TeamMember
                {
                    Id = 4,
                    FirstName = "Oliver",
                    LastName = "Kahn",
                    Email = "oliver.kahn@qvc.com"
                },
                new TeamMember
                {
                    Id = 5,
                    FirstName = "Test",
                    LastName = "Member",
                    Email = "test.test@qvc.com",
                    IsActive = false
                },
            };

        public TeamMember GetTeamMemberById(int teamMemberId)
        {
            return AllMembers.FirstOrDefault(tm => tm.Id == teamMemberId);
        }
    }
}
