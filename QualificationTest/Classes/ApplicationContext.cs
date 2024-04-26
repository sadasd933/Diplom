using System.Data.Entity;

namespace QualificationTest
{
    class ApplicationContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Question> Questions { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }

    }
}
