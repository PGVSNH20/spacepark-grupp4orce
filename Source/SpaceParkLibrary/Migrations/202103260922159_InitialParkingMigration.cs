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
                        ParkingFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AssignedParkingLot_Id = c.Int(),
                        CustomerId_Id = c.Int(),
                        StarshipId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkingLots", t => t.AssignedParkingLot_Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId_Id)
                .ForeignKey("dbo.Starships", t => t.StarshipId_Id)
                .Index(t => t.AssignedParkingLot_Id)
                .Index(t => t.CustomerId_Id)
                .Index(t => t.StarshipId_Id);
            
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
            DropForeignKey("dbo.ParkingOrders", "StarshipId_Id", "dbo.Starships");
            DropForeignKey("dbo.ParkingOrders", "CustomerId_Id", "dbo.Customers");
            DropForeignKey("dbo.ParkingOrders", "AssignedParkingLot_Id", "dbo.ParkingLots");
            DropIndex("dbo.ParkingOrders", new[] { "StarshipId_Id" });
            DropIndex("dbo.ParkingOrders", new[] { "CustomerId_Id" });
            DropIndex("dbo.ParkingOrders", new[] { "AssignedParkingLot_Id" });
            DropTable("dbo.Starships");
            DropTable("dbo.ParkingOrders");
            DropTable("dbo.ParkingLots");
            DropTable("dbo.Customers");
        }
    }
}
