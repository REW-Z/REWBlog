namespace REW的空间Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NotificationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NOTIFICATIONS",
                c => new
                    {
                        NID = c.Int(nullable: false, identity: true),
                        USERNAME = c.String(nullable: false),
                        NOTI_TYPE = c.Int(nullable: false),
                        MESSAGE = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NID);
        }
        
        public override void Down()
        {
            DropTable("dbo.NOTIFICATIONS");
        }
    }
}
