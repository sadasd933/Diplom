using QualificationTest.Classes;
using System.Data.Entity;
using System.Data.Entity.Hierarchy;

namespace QualificationTest
{
    class ApplicationContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        

        public ApplicationContext() : base("DefaultConnection") { }

    }
}
