using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    public class Contexto : DbContext
    {
        public Contexto(): base("name=BusinessViewsConnection")
        {
            Database.SetInitializer<Contexto>(new CreateDatabaseIfNotExists<Contexto>());
        
        //Database.SetInitializer<Contexto>(new DropCreateDatabaseIfModelChanges<Contexto>());
        }

        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Invercionista> Invercionistas { get; set; }
        public DbSet<Cierre> Cierres { get; set; }
        public DbSet<AlmacenInversionista> AlmacenInversionistas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}