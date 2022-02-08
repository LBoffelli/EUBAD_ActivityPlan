using System;
using System.ComponentModel.DataAnnotations;

namespace EUBAD_ActivityPlan.Models
{
    public class TeamMemberActivity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Day { get; set; } = DateTime.Today;

        [Required]
        public int TeamMemberId { get; set; }
        [Required]
        public TeamMember TeamMember { get; set; }

        [Required]
        public int ActivityId { get; set; }
        [Required]
        public Activity Activity { get; set; }
    }
}
