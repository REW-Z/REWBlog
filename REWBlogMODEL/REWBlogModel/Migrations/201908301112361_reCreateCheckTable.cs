namespace REW的空间Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reCreateCheckTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CHECKINs",
                c => new
                    {
                        CheckID = c.Int(nullable: false, identity: true),
                        USERNAME = c.String(nullable: false),
                        LASTCHECKIN = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CheckID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CHECKINs");
        }
    }
}
