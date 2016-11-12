namespace CapaDeDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemodel1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cierre", new[] { "Fecha" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Cierre", "Fecha", unique: true);
        }
    }
}
