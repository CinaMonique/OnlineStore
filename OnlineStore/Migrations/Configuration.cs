namespace OnlineStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineStore.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "OnlineStore.Models.ApplicationDbContext";
        }

        protected override void Seed(OnlineStore.Models.ApplicationDbContext context)
        {

        }
    }
}
