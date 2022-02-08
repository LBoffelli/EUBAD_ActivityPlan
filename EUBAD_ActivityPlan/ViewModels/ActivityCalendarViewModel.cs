using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace EUBAD_ActivityPlan.ViewModels
{
    public class ActivityCalendarViewModel
    {
        public int TeamMemberId { get; set; }
        public int ActivityId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<SelectListItem> TeamMembers { get; set; }
        public IEnumerable<SelectListItem> Activities { get; set; }
    }
}
