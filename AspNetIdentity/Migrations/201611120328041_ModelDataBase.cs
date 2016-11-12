namespace AspNetIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelDataBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PrimerNombre", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "PrimerApellido", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "SegundoNombre", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "SegundoApellido", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "LicenseExpiration", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "Level");
            DropColumn("dbo.AspNetUsers", "JoinDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "JoinDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Level", c => c.Byte(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AspNetUsers", "LicenseExpiration");
            DropColumn("dbo.AspNetUsers", "SegundoApellido");
            DropColumn("dbo.AspNetUsers", "SegundoNombre");
            DropColumn("dbo.AspNetUsers", "PrimerApellido");
            DropColumn("dbo.AspNetUsers", "PrimerNombre");
        }
    }
}
