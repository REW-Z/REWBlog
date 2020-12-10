namespace REW的空间Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCheckTable : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.CHECKINs");
        }
    }
}
