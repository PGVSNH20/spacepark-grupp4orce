namespace SpaceParkLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialParkingMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        InvoicePaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkingLots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Occupied = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkingOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArrivalTime = c.DateTime(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        AssignedParkingLotId = c.Int(nullable: false),
                        ParkingFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.Int(),
                        Starship_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Starships", t => t.Starship_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Starship_Id);
            
            CreateTable(
                "dbo.Starships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkingOrders", "Starship_Id", "dbo.Starships");
            DropForeignKey("dbo.ParkingOrders", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.ParkingOrders", new[] { "Starship_Id" });
            DropIndex("dbo.ParkingOrders", new[] { "Customer_Id" });
            DropTable("dbo.Starships");
            DropTable("dbo.ParkingOrders");
            DropTable("dbo.ParkingLots");
            DropTable("dbo.Customers");
        }
    }
}
