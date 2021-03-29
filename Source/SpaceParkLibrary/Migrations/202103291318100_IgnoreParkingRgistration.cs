namespace SpaceParkLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IgnoreParkingRgistration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "ParkingRegistration_Id", "dbo.ParkingOrders");
            DropIndex("dbo.Customers", new[] { "ParkingRegistration_Id" });
            DropColumn("dbo.Customers", "ParkingRegistration_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "ParkingRegistration_Id", c => c.Int());
            CreateIndex("dbo.Customers", "ParkingRegistration_Id");
            AddForeignKey("dbo.Customers", "ParkingRegistration_Id", "dbo.ParkingOrders", "Id");
        }
    }
}
