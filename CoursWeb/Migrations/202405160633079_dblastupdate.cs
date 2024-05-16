namespace CoursWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dblastupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuizID = c.Int(nullable: false, identity: true),
                        LessonID = c.Int(),
                        Questions_number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuizID)
                .ForeignKey("dbo.Lessons", t => t.LessonID)
                .Index(t => t.LessonID);
            
            CreateTable(
                "dbo.QuizDetails",
                c => new
                    {
                        QuizDetailID = c.Int(nullable: false, identity: true),
                        QuizID = c.Int(),
                        Question = c.String(nullable: false, maxLength: 100),
                        Answer = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.QuizDetailID)
                .ForeignKey("dbo.Quizs", t => t.QuizID)
                .Index(t => t.QuizID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuizDetails", new[] { "QuizID" });
            DropIndex("dbo.Quizs", new[] { "LessonID" });
            DropForeignKey("dbo.QuizDetails", "QuizID", "dbo.Quizs");
            DropForeignKey("dbo.Quizs", "LessonID", "dbo.Lessons");
            DropTable("dbo.QuizDetails");
            DropTable("dbo.Quizs");
        }
    }
}
