namespace Wspolnota.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Wspolnota.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "1@e.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "1@e.com",
                    Email = "1@e.com",
                    FirstName = "Ad",
                    LastName = "min",
                    City = "Admins Valley",
                    Gender = true
                };

                manager.Create(user, "123456"); //u篡tkownik, has這
                manager.AddToRole(user.Id, "Admin"); //u篡tkownik, rola
            }

            if (!context.Communities.Any(c => c.Name == "W鏊cza雟ka 217"))
            {
                context.Communities.AddOrUpdate(new Community
                {
                    Name = "W鏊cza雟ka 217",
                    Image = "https://uml.lodz.pl/files/public/_processed_/a/8/csm_Wysoka_blok06_f45fe583cc.jpg",
                    Posts = new List<Post>
                    {
                        new Announcement
                        {
                            Title = "Pierwsze og這szenie",
                            Author = context.Users.Where(u=>u.UserName == "1@e.com").FirstOrDefault(),
                            CreatedAt = DateTime.Now,
                            Content = "Tekst og這szenia"
                        },
                        new Survey
                        {
                            Title = "Pierwsza ankieta - Co by這 pierwsze, jajko czy kura?",
                            Author = context.Users.Where(u=>u.UserName == "1@e.com").FirstOrDefault(),
                            CreatedAt = DateTime.Now,
                            Answers = new List<Answer>
                            {
                                new Answer { Content = "Jajko"},
                                new Answer { Content = "Kura" }
                            }
                        },
                        new Brochure
                        {
                            Title = "Pierwsza reklama",
                            Author = context.Users.Where(u=>u.UserName == "1@e.com").FirstOrDefault(),
                            CreatedAt = DateTime.Now,
                            Image = @"https://pbs.twimg.com/media/DtxH42HXQAAb5Ks.jpg",
                            Link = @"https://www.google.com"
                        }
                    },
                    Users = new List<ApplicationUser>
                    {
                        context.Users.Where(u => u.UserName == "1@e.com").FirstOrDefault()
                    }
                });
            }

            context.SaveChanges();
        }
    }
}
