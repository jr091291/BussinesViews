namespace CapaDeDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrador",
                c => new
                    {
                        AdministradorId = c.String(nullable: false, maxLength: 128),
                        AlmacenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdministradorId)
                .ForeignKey("dbo.Almacen", t => t.AlmacenId, cascadeDelete: true)
                .Index(t => t.AlmacenId);
            
            CreateTable(
                "dbo.Almacen",
                c => new
                    {
                        AlmacenId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Direccion = c.String(nullable: false, maxLength: 60),
                        Telefono = c.String(nullable: false, maxLength: 13),
                        Correo = c.String(maxLength: 80),
                        Invercionista_InvercionistaId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AlmacenId)
                .ForeignKey("dbo.Invercionista", t => t.Invercionista_InvercionistaId)
                .Index(t => t.Nombre, unique: true)
                .Index(t => t.Invercionista_InvercionistaId);
            
            CreateTable(
                "dbo.AlmacenInversionista",
                c => new
                    {
                        AlmacenId = c.Int(nullable: false),
                        InversionistaId = c.String(nullable: false, maxLength: 128),
                        Invercionista_InvercionistaId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.AlmacenId, t.InversionistaId })
                .ForeignKey("dbo.Almacen", t => t.AlmacenId, cascadeDelete: true)
                .ForeignKey("dbo.Invercionista", t => t.Invercionista_InvercionistaId)
                .Index(t => t.AlmacenId)
                .Index(t => t.Invercionista_InvercionistaId);
            
            CreateTable(
                "dbo.Invercionista",
                c => new
                    {
                        InvercionistaId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.InvercionistaId);
            
            CreateTable(
                "dbo.Cierre",
                c => new
                    {
                        CierreId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Efectivo = c.Double(nullable: false),
                        Bancos = c.Double(nullable: false),
                        Facturas = c.Double(nullable: false),
                        Invercion = c.Double(nullable: false),
                        Costos = c.Double(nullable: false),
                        AlmacenId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CierreId)
                .ForeignKey("dbo.Almacen", t => t.AlmacenId, cascadeDelete: true)
                .Index(t => t.Fecha, unique: true)
                .Index(t => t.AlmacenId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cierre", "AlmacenId", "dbo.Almacen");
            DropForeignKey("dbo.AlmacenInversionista", "Invercionista_InvercionistaId", "dbo.Invercionista");
            DropForeignKey("dbo.Almacen", "Invercionista_InvercionistaId", "dbo.Invercionista");
            DropForeignKey("dbo.AlmacenInversionista", "AlmacenId", "dbo.Almacen");
            DropForeignKey("dbo.Administrador", "AlmacenId", "dbo.Almacen");
            DropIndex("dbo.Cierre", new[] { "AlmacenId" });
            DropIndex("dbo.Cierre", new[] { "Fecha" });
            DropIndex("dbo.AlmacenInversionista", new[] { "Invercionista_InvercionistaId" });
            DropIndex("dbo.AlmacenInversionista", new[] { "AlmacenId" });
            DropIndex("dbo.Almacen", new[] { "Invercionista_InvercionistaId" });
            DropIndex("dbo.Almacen", new[] { "Nombre" });
            DropIndex("dbo.Administrador", new[] { "AlmacenId" });
            DropTable("dbo.Cierre");
            DropTable("dbo.Invercionista");
            DropTable("dbo.AlmacenInversionista");
            DropTable("dbo.Almacen");
            DropTable("dbo.Administrador");
        }
    }
}
