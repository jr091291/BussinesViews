namespace CapaDeDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Almacen", "Invercionista_InvercionistaId", "dbo.Invercionista");
            DropIndex("dbo.Almacen", new[] { "Invercionista_InvercionistaId" });
            DropColumn("dbo.Almacen", "Invercionista_InvercionistaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Almacen", "Invercionista_InvercionistaId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Almacen", "Invercionista_InvercionistaId");
            AddForeignKey("dbo.Almacen", "Invercionista_InvercionistaId", "dbo.Invercionista", "InvercionistaId");
        }
    }
}
