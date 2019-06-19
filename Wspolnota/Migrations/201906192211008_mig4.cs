namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Communities", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Communities", "Description");
        }
    }
}
