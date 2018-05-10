namespace AllianceUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMiddleName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "MiddleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "MiddleName");
        }
    }
}
