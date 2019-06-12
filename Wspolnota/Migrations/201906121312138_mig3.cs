namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropColumn("dbo.Posts", "AuthorId");
            RenameColumn(table: "dbo.Posts", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.Posts", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "AuthorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            AlterColumn("dbo.Posts", "AuthorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Posts", name: "AuthorId", newName: "Author_Id");
            AddColumn("dbo.Posts", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "Author_Id");
        }
    }
}
