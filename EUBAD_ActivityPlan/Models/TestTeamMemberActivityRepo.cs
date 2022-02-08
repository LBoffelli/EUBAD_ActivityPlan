using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUBAD_ActivityPlan.Models
{
    public class TestTeamMemberActivityRepo : ITeamMemberActivityRepo
    {
        TestTeamMemberRepo teamMemberRepo = new TestTeamMemberRepo();
        TestActivityRepo activityRepo = new TestActivityRepo();
        public IEnumerable<TeamMemberActivity> AllTeamActivities =>
            new List<TeamMemberActivity>
            {
                new TeamMemberActivity
                {
                    Id = 1,
                    TeamMemberId = 1,
                    ActivityId = 1,
                    Day = DateTime.Today,
                    TeamMember = teamMemberRepo.GetTeamMemberById(1),
                    Activity = activityRepo.GetActivityById(1)
                },
                new TeamMemberActivity
                {
                    Id = 2,
                    TeamMemberId = 2,
                    ActivityId = 1,
                    Day = DateTime.Today,
                    TeamMember = teamMemberRepo.GetTeamMemberById(2),
                    Activity = activityRepo.GetActivityById(1)
                },
                new TeamMemberActivity
                {
                    Id = 3,
                    TeamMemberId = 3,
                    ActivityId = 4,
                    Day = DateTime.Today,
                    TeamMember = teamMemberRepo.GetTeamMemberById(3),
                    Activity = activityRepo.GetActivityById(4)
                },
                new TeamMemberActivity
                {
                    Id = 4,
                    TeamMemberId = 3,
                    ActivityId = 2,
                    Day = DateTime.Parse("28/1/2022"),
                    TeamMember = teamMemberRepo.GetTeamMemberById(3),
                    Activity = activityRepo.GetActivityById(2)
                },
                new TeamMemberActivity
                {
                    Id = 5,
                    TeamMemberId = 2,
                    ActivityId = 3,
                    Day = DateTime.Parse("28/2/2022"),
                    TeamMember = teamMemberRepo.GetTeamMemberById(2),
                    Activity = activityRepo.GetActivityById(3)
                },
            };

        public TeamMemberActivity GetTeamMemberActivityById(int teamMemberActivityId)
        {
            return AllTeamActivities.FirstOrDefault(tma => tma.Id == teamMemberActivityId);
        }
        public IEnumerable<TeamMemberActivity> GetTeamMemberActivitiesByDate(DateTime startDate, DateTime endDate)
        {
            return AllTeamActivities.Where(tma => tma.Day.Date >= startDate.Date && tma.Day.Date <= endDate.Date);
        }

    }
}
