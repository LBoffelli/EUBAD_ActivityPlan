using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EUBAD_ActivityPlan.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<TeamMemberActivity> TeamMemberActivities { get; set; }
    }
}
