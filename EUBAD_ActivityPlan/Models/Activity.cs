using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EUBAD_ActivityPlan.Models
{
    public class Activity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public IEnumerable<TeamMemberActivity> TeamMemberActivities { get; set; }

    }
}
