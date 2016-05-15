using System.Data.Entity.Migrations;
using Caerus.Modules.Authentication.Service.Repository.Context;

namespace Caerus.Modules.Authentication.Service.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CaerusIdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CaerusIdentityContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
