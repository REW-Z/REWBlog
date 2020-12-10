namespace REW的空间Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newRELATIONS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RELATIONS",
                c => new
                    {
                        R_ID = c.Int(nullable: false, identity: true),
                        R_FollowID = c.Int(nullable: false),
                        R_FocusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.R_ID);
            
            DropColumn("dbo.USERS", "PIC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.USERS", "PIC", c => c.String());
            DropTable("dbo.RELATIONS");
        }
    }
}
