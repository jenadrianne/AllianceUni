namespace AllianceUniversity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AllianceUniversity.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AllianceUniversity.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AllianceUniversity.DAL.SchoolContext";
        }

        protected override void Seed(AllianceUniversity.DAL.SchoolContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Students.AddOrUpdate(x => x.Id,
                new Students
                {
                    FirstName = "Text", 
                    LastName = "TextSad", 
                    EnrollmentDate = DateTime.Parse("05/10/2018")
                }
            );

            context.SaveChanges();
        }
    }
}
