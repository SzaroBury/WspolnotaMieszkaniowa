namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserCommunities", newName: "CommunityApplicationUsers");
            DropPrimaryKey("dbo.CommunityApplicationUsers");
            AddPrimaryKey("dbo.CommunityApplicationUsers", new[] { "Community_CommunityID", "ApplicationUser_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CommunityApplicationUsers");
            AddPrimaryKey("dbo.CommunityApplicationUsers", new[] { "ApplicationUser_Id", "Community_CommunityID" });
            RenameTable(name: "dbo.CommunityApplicationUsers", newName: "ApplicationUserCommunities");
        }
    }
}
