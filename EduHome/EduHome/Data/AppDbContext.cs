using EduHome.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace EduHome.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderDetail> SliderDetails { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<CourseFeatures> CourseFeatures { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDetail> EventDetails { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TeacherSkill> TeacherSkills { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }


    }
}
