namespace REW的空间Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delCheckTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CHECKINs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CHECKINs",
                c => new
                    {
                        UID = c.Int(nullable: false, identity: true),
                        LASTCHECKIN = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UID);
            
        }
    }
}
