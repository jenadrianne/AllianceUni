namespace AllianceUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Course", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Course", "Title", c => c.Int(nullable: false));
        }
    }
}
