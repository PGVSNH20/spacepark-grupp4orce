namespace SpaceParkLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitiaParkingMigration : DbMigration
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
                "dbo.ParkingOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        StarshipId = c.Int(nullable: false),
                        ArrivalTime = c.DateTime(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        AssignedParkingLotId = c.Int(nullable: false),
                        ParkingFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkingLots", t => t.AssignedParkingLotId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Starships", t => t.StarshipId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.StarshipId)
                .Index(t => t.AssignedParkingLotId);
            
            CreateTable(
                "dbo.ParkingLots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Occupied = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.ParkingOrders", "StarshipId", "dbo.Starships");
            DropForeignKey("dbo.ParkingOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ParkingOrders", "AssignedParkingLotId", "dbo.ParkingLots");
            DropIndex("dbo.ParkingOrders", new[] { "AssignedParkingLotId" });
            DropIndex("dbo.ParkingOrders", new[] { "StarshipId" });
            DropIndex("dbo.ParkingOrders", new[] { "CustomerId" });
            DropTable("dbo.Starships");
            DropTable("dbo.ParkingLots");
            DropTable("dbo.ParkingOrders");
            DropTable("dbo.Customers");
        }
    }
}
