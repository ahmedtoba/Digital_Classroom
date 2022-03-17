using Digital_Classroom.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Digital_Classroom.Data
{
    public class ClassroomContext : IdentityDbContext
    {
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Video> Videos { get; set; }

        public ClassroomContext(DbContextOptions options) : base(options)
        {
        }

    }
}
