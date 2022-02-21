using EUBAD_ActivityPlan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace EUBAD_ActivityPlan.ViewModels
{
    public class ActivityCalendarViewModel
    {
        public int SelectedTeamMemberId { get; set; }
        public int SelectedActivityId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<TeamMember> TeamMembers { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
    }
}
