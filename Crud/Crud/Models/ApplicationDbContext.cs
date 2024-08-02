using Microsoft.EntityFrameworkCore;

namespace Crud.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<student> student { get; set; }
        public DbSet<Student_data> student_Data { get; set; }

    }
}
