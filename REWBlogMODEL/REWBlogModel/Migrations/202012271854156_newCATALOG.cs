namespace REWBlogModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newCATALOG : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ARTICLES", "A_CATALOG", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ARTICLES", "A_CATALOG");
        }
    }
}
