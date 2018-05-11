namespace AllianceUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrollmentChanges : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Enrollment");
            AlterColumn("dbo.Enrollment", "EnrollmentId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Enrollment", "EnrollmentId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Enrollment");
            AlterColumn("dbo.Enrollment", "EnrollmentId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Enrollment", "EnrollmentId");
        }
    }
}
