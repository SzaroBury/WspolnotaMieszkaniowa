namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CommunityID", "dbo.Communities");
            DropIndex("dbo.AspNetUsers", new[] { "CommunityID" });
            AlterColumn("dbo.AspNetUsers", "CommunityId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CommunityId");
            AddForeignKey("dbo.AspNetUsers", "CommunityId", "dbo.Communities", "CommunityID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CommunityId", "dbo.Communities");
            DropIndex("dbo.AspNetUsers", new[] { "CommunityId" });
            AlterColumn("dbo.AspNetUsers", "CommunityId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CommunityID");
            AddForeignKey("dbo.AspNetUsers", "CommunityID", "dbo.Communities", "CommunityID", cascadeDelete: true);
        }
    }
}
