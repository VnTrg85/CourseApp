using System.Data.Entity;

namespace CoursWeb.Models
{
    public class DataContext:DbContext
    {
        public DataContext() : base("CoursWeb")
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<QuizDetail> QuizDetails { get; set; }

    }
}