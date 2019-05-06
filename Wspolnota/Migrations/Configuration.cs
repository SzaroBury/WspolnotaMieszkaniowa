namespace Wspolnota.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.Linq;
    using Wspolnota.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //var temp = Image.FromFile(@"C:\Users\Matej\Desktop\Repo\WspolnotaMieszkaniowa\Wspolnota\App_Data\blok_w_£odzi.jpg");
            //ImageConverter imageConverter = new ImageConverter();

            //context.Communities.AddOrUpdate(new Community
            //{
            //    CommunityID = 1,
            //    Name = "ul. Wymyœlona 217",
            //    Image = (byte[])imageConverter.ConvertTo(temp, typeof(byte[]))
            //});

            //Announcement announcement = new Announcement
            //{
            //    ID = 1,
            //    Title = "Tytu³ og³oszenia",
            //    Content = "blablablablablablablablablablablablablablablablablablablablablablablablablablablablablablablabla",
            //    Date = DateTime.Now
            //};

            //context.Communities.First().Announcements.Add(announcement);
        }
    }
}
