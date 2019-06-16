namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AuthorId = c.String(maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                        CommunityId = c.Int(nullable: false),
                        Content = c.String(),
                        Link = c.String(),
                        Image = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Communities", t => t.CommunityId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.CommunityId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        PostalCode = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Comment_CommentId = c.Int(),
                        Post_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Comment_CommentId)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Communities",
                c => new
                    {
                        CommunityID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.CommunityID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.Author_Id);
            
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
                        AuthorId = c.String(maxLength: 128),
                        Survey_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Posts", t => t.Survey_PostId)
                .Index(t => t.AnswerId)
                .Index(t => t.AuthorId)
                .Index(t => t.Survey_PostId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.CommunityApplicationUsers",
                c => new
                    {
                        Community_CommunityID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Community_CommunityID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Communities", t => t.Community_CommunityID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Community_CommunityID)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommunityApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommunityApplicationUsers", "Community_CommunityID", "dbo.Communities");
            DropForeignKey("dbo.Votes", "Survey_PostId", "dbo.Posts");
            DropForeignKey("dbo.Votes", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Answers", "Survey_PostId", "dbo.Posts");
            DropForeignKey("dbo.AspNetUsers", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CommunityId", "dbo.Communities");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.AspNetUsers", "Comment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CommunityApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CommunityApplicationUsers", new[] { "Community_CommunityID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Votes", new[] { "Survey_PostId" });
            DropIndex("dbo.Votes", new[] { "AuthorId" });
            DropIndex("dbo.Votes", new[] { "AnswerId" });
            DropIndex("dbo.Answers", new[] { "Survey_PostId" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Post_PostId" });
            DropIndex("dbo.AspNetUsers", new[] { "Comment_CommentId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Posts", new[] { "CommunityId" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropTable("dbo.CommunityApplicationUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Votes");
            DropTable("dbo.Answers");
            DropTable("dbo.Comments");
            DropTable("dbo.Communities");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Posts");
        }
    }
}
