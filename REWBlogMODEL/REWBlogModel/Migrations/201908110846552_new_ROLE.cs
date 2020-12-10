namespace REW的空间Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_ROLE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.USERS", "ROLE", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.USERS", "ROLE");
        }
    }
}
