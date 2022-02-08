using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EUBAD_ActivityPlan.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public string Password { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;

        public IEnumerable<TeamMemberActivity> TeamMemberActivities { get; set; }
    }
}
