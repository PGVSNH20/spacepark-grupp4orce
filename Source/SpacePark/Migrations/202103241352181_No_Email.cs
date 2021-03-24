namespace UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class No_Email : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Email", c => c.String());
        }
    }
}
