namespace CoursWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updbone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enrolments",
                c => new
                    {
                        EnrolmentID = c.Int(nullable: false, identity: true),
                        Enrolment_date = c.DateTime(nullable: false),
                        Completed_date = c.DateTime(nullable: false),
                        AccountID = c.Int(),
                        CourseID = c.Int(),
                    })
                .PrimaryKey(t => t.EnrolmentID)
                .ForeignKey("dbo.Accounts", t => t.AccountID)
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .Index(t => t.AccountID)
                .Index(t => t.CourseID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Enrolments", new[] { "CourseID" });
            DropIndex("dbo.Enrolments", new[] { "AccountID" });
            DropForeignKey("dbo.Enrolments", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Enrolments", "AccountID", "dbo.Accounts");
            DropTable("dbo.Enrolments");
        }
    }
}
