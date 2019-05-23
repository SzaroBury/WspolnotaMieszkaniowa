namespace Wspolnota.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using Wspolnota.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
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
            context.Roles.AddOrUpdate(new IdentityRole() { Name = "Admin" });
            context.Roles.AddOrUpdate(new IdentityRole() { Name = "Registered" });
            var user = new ApplicationUser { UserName = "1@e.com", Email = "1@e.com", FirstName = "Ad", LastName = "min", City = "Admins Valley", Gender = 'M' };
            //var result = await UserManager<ApplicationUser>.CreateAsync(user, "123");
            //context.Users.AddOrUpdate(new Application)
            context.SaveChanges();
        }
    }
}
