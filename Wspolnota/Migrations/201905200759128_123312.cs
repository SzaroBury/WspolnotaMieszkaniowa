namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123312 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Communities", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Communities", "Image", c => c.Binary(nullable: false));
        }
    }
}
