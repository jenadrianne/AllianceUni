namespace AllianceUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "LastName", c => c.String());
        }
    }
}
