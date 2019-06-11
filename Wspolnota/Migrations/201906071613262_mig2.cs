namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        Content = c.String(),
                        Survey_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Posts", t => t.Survey_PostId)
                .Index(t => t.Survey_PostId);
            
            AddColumn("dbo.SurveyAnswers", "Answer_AnswerId", c => c.Int());
            CreateIndex("dbo.SurveyAnswers", "Answer_AnswerId");
            AddForeignKey("dbo.SurveyAnswers", "Answer_AnswerId", "dbo.Answers", "AnswerId");
            DropColumn("dbo.SurveyAnswers", "SurveyId");
            DropColumn("dbo.SurveyAnswers", "Answer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SurveyAnswers", "Answer", c => c.Int(nullable: false));
            AddColumn("dbo.SurveyAnswers", "SurveyId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SurveyAnswers", "Answer_AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Answers", "Survey_PostId", "dbo.Posts");
            DropIndex("dbo.Answers", new[] { "Survey_PostId" });
            DropIndex("dbo.SurveyAnswers", new[] { "Answer_AnswerId" });
            DropColumn("dbo.SurveyAnswers", "Answer_AnswerId");
            DropTable("dbo.Answers");
        }
    }
}
