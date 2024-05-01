namespace CoursWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonID = c.Int(nullable: false, identity: true),
                        LessonName = c.String(nullable: false, maxLength: 100),
                        Lesson_URL = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CourseID = c.Int(),
                    })
                .PrimaryKey(t => t.LessonID)
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .Index(t => t.CourseID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Lessons", new[] { "CourseID" });
            DropForeignKey("dbo.Lessons", "CourseID", "dbo.Courses");
            DropTable("dbo.Lessons");
        }
    }
}
