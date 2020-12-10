namespace REW的空间Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_whose : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NOTIFICATIONS", "WHOSE", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NOTIFICATIONS", "WHOSE");
        }
    }
}
