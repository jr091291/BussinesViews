namespace CapaDeDatos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CapaDeDatos.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CapaDeDatos.Contexto";
        }

        protected override void Seed(CapaDeDatos.Contexto context)
        {
            context.Almacenes.Add(new Almacen
            {
                AlmacenId = 1,
                Correo = "1661@gmail.com",
                Direccion = "166",
                Nombre = "fruvar 166",
                Telefono = "23456788"
            });
        }
    }
}
