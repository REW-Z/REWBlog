namespace REW的空间Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_Points : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.USERS", "POINTS", c => c.Int(nullable: false, defaultValue: 0));
        }

        public override void Down()
        {
            DropColumn("dbo.USERS", "POINTS");
        }
    }
}
