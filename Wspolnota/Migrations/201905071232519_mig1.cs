namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Communities", "Image", c => c.Binary(nullable: false, storeType: "image"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Communities", "Image", c => c.Binary(nullable: false));
        }
    }
}
