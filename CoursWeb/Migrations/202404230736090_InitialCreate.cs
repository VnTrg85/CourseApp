namespace CoursWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        Image = c.String(),
                        Price = c.Double(),
                        CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Courses", new[] { "CategoryID" });
            DropForeignKey("dbo.Courses", "CategoryID", "dbo.Categories");
            DropTable("dbo.Courses");
            DropTable("dbo.Categories");
        }
    }
}
