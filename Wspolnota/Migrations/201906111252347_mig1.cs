namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SurveyAnswers", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SurveyAnswers", "Survey_PostId", "dbo.Posts");
            DropIndex("dbo.SurveyAnswers", new[] { "Author_Id" });
            DropIndex("dbo.SurveyAnswers", new[] { "Survey_PostId" });
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
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        AnswerId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                        Survey_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Posts", t => t.Survey_PostId)
                .Index(t => t.AnswerId)
                .Index(t => t.Author_Id)
                .Index(t => t.Survey_PostId);
            
            DropTable("dbo.SurveyAnswers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SurveyAnswers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        Answer = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                        Survey_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerId);
            
            DropForeignKey("dbo.Votes", "Survey_PostId", "dbo.Posts");
            DropForeignKey("dbo.Votes", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Answers", "Survey_PostId", "dbo.Posts");
            DropIndex("dbo.Votes", new[] { "Survey_PostId" });
            DropIndex("dbo.Votes", new[] { "Author_Id" });
            DropIndex("dbo.Votes", new[] { "AnswerId" });
            DropIndex("dbo.Answers", new[] { "Survey_PostId" });
            DropTable("dbo.Votes");
            DropTable("dbo.Answers");
            CreateIndex("dbo.SurveyAnswers", "Survey_PostId");
            CreateIndex("dbo.SurveyAnswers", "Author_Id");
            AddForeignKey("dbo.SurveyAnswers", "Survey_PostId", "dbo.Posts", "PostId");
            AddForeignKey("dbo.SurveyAnswers", "Author_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
