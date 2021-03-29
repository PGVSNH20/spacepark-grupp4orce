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
                        ParkingRegistration_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParkingOrders", t => t.ParkingRegistration_Id)
                .Index(t => t.ParkingRegistration_Id);
            
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
            DropForeignKey("dbo.Customers", "ParkingRegistration_Id", "dbo.ParkingOrders");
            DropIndex("dbo.Customers", new[] { "ParkingRegistration_Id" });
            DropTable("dbo.Starships");
            DropTable("dbo.ParkingLots");
            DropTable("dbo.ParkingOrders");
            DropTable("dbo.Customers");
        }
    }
}
