namespace CoursWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "isAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "isAdmin");
        }
    }
}
