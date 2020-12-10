namespace REW的空间Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsRead : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NOTIFICATIONS", "READ", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NOTIFICATIONS", "READ");
        }
    }
}
