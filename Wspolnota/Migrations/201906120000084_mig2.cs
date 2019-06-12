namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Votes", new[] { "Author_Id" });
            DropColumn("dbo.Votes", "AuthorId");
            RenameColumn(table: "dbo.Votes", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.Votes", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Votes", "AuthorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Votes", new[] { "AuthorId" });
            AlterColumn("dbo.Votes", "AuthorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Votes", name: "AuthorId", newName: "Author_Id");
            AddColumn("dbo.Votes", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "Author_Id");
        }
    }
}
