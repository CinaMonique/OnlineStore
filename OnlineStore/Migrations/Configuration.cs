namespace OnlineStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "OnlineStore.Models.ApplicationDbContext";
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {

        }
    }
}
