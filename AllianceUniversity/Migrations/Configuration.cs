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

            if (!context.Students.Any())
            {
                context.Students.AddOrUpdate(x => x.Id,
                    new Students
                    {
                        FirstName = "Text",
                        LastName = "TextSad",
                        EnrollmentDate = DateTime.Parse("05/10/2018")
                    },
                    new Students
                    {
                        FirstName = "Text2",
                        LastName = "TextSad2",
                        EnrollmentDate = DateTime.Parse("05/10/2018")
                    }
                );

                context.SaveChanges();
            }

            if (!context.Course.Any())
            {
                context.Course.AddOrUpdate(x => x.CourseId,
                    new Course
                    {
                        CourseId = 10001,
                        Title = "Test",
                        Credits = 5
                    },
                     new Course
                     {
                         CourseId = 10002,
                         Title = "Test2",
                         Credits = 4
                     }
                    );

                context.SaveChanges();
            }


            if (!context.Enrollment.Any())
            {
                context.Enrollment.AddOrUpdate(x => x.EnrollmentId,
                    new Enrollment
                    {
                        EnrollmentId = 2001,
                        CourseId = 10001,
                        StudentId = 1,
                        Grade = Grade.A
                    },
                    new Enrollment
                    {
                        EnrollmentId = 2002,
                        CourseId = 10001,
                        StudentId = 2,
                        Grade = Grade.B
                    }
                );

                context.SaveChanges();
            }

            context.SaveChanges();
        }
    }
}
