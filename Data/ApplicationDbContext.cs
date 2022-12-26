using dotnetdevs.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnetdevs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ExperienceLevel> ExperienceLevels { get; set; }
        public DbSet<SearchStatus> SearchStatuses { get; set; }
		public DbSet<Conversation> Conversations { get; set; }
		public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }

        #region Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SearchStatus>().HasData(
                new SearchStatus { ID = 1, Name = "Actively looking", IsDefault=true, Description = "Your profile can get featured on the homepage." },
                new SearchStatus { ID = 2, Name = "Open", Description = "Your profile can get featured on the homepage." },
                new SearchStatus { ID = 3, Name = "Not interested", Description = "Your profile will not appear in search results." },
                new SearchStatus { ID = 4, Name = "Invisible", Description = "Your profile is hidden and can only be seen by yourself." }
            );
            modelBuilder.Entity<ExperienceLevel>().HasData(
                new ExperienceLevel { ID = 1, Name = "Junior" },
                new ExperienceLevel { ID = 2, Name = "Mid-level", IsDefault = true },
                new ExperienceLevel { ID = 3, Name = "Senior" },
                new ExperienceLevel { ID = 4, Name = "Lead" }
            );
        }
        #endregion
    }
}