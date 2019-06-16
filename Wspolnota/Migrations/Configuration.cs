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

                manager.Create(user, "123456"); //u¿ytkownik, has³o
                manager.AddToRole(user.Id, "Admin"); //u¿ytkownik, rola
            }

            if (!context.Users.Any(u => u.UserName == "2@e.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "2@e.com",
                    Email = "2@e.com",
                    FirstName = "Mateusz",
                    LastName = "Kubicki",
                    City = "£ódŸ",
                    Gender = true
                };

                manager.Create(user, "123456"); //u¿ytkownik, has³o
            }


            if (!context.Communities.Any(c => c.Name == "Wólczañska 217"))
            {
                context.Communities.AddOrUpdate(new Community
                {
                    Name = "Wólczañska 217",
                    Image = "https://uml.lodz.pl/files/public/_processed_/a/8/csm_Wysoka_blok06_f45fe583cc.jpg",
                    Posts = new List<Post>
                    {
                        new Announcement
                        {
                            Title = "Pierwsze og³oszenie",
                            Author = context.Users.Where(u=>u.UserName == "1@e.com").FirstOrDefault(),
                            CreatedAt = DateTime.Now,
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec non nunc hendrerit, auctor diam eleifend, malesuada elit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Phasellus lobortis congue sem. Maecenas viverra massa vitae ante egestas gravida. In ante turpis, pretium at nisi sit amet, sagittis venenatis sapien. Maecenas ultrices, eros ac posuere eleifend, dui ipsum luctus nunc, et euismod mauris orci tempor tortor. Praesent mollis ante quis orci eleifend scelerisque eget pellentesque eros. Nulla eget urna erat. Duis nec arcu hendrerit tortor sollicitudin tempor in eget justo."
                        },
                        new Survey
                        {
                            Title = "Pierwsza ankieta - Co by³o pierwsze, jajko czy kura?",
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

            if (!context.Communities.Any(c => c.Name == "Wólczañska 219"))
            {
                context.Communities.AddOrUpdate(new Community
                {
                    Name = "Wólczañska 219",
                    Image = "https://uml.lodz.pl/files/public/_processed_/a/8/csm_Wysoka_blok06_f45fe583cc.jpg",
                    Posts = new List<Post>
                    {
                        new Announcement
                        {
                            Title = "Pierwsze og³oszenie",
                            Author = context.Users.Where(u=>u.UserName == "2@e.com").FirstOrDefault(),
                            CreatedAt = DateTime.Now,
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec non nunc hendrerit, auctor diam eleifend, malesuada elit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Phasellus lobortis congue sem. Maecenas viverra massa vitae ante egestas gravida. In ante turpis, pretium at nisi sit amet, sagittis venenatis sapien. Maecenas ultrices, eros ac posuere eleifend, dui ipsum luctus nunc, et euismod mauris orci tempor tortor. Praesent mollis ante quis orci eleifend scelerisque eget pellentesque eros. Nulla eget urna erat. Duis nec arcu hendrerit tortor sollicitudin tempor in eget justo."
                        },
                        new Survey
                        {
                            Title = "Pierwsza ankieta - Co by³o pierwsze, jajko czy kura?",
                            Author = context.Users.Where(u=>u.UserName == "2@e.com").FirstOrDefault(),
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
                            Author = context.Users.Where(u=>u.UserName == "2@e.com").FirstOrDefault(),
                            CreatedAt = DateTime.Now,
                            Image = @"https://pbs.twimg.com/media/DtxH42HXQAAb5Ks.jpg",
                            Link = @"https://www.google.com"
                        }
                    },
                    Users = new List<ApplicationUser>
                    {
                        context.Users.Where(u => u.UserName == "2@e.com").FirstOrDefault()
                    }
                });
            }

            context.SaveChanges();
        }
    }
}
