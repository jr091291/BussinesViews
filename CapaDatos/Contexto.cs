using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Contexto : DbContext
    {
        public Contexto():base("name: ")
        {
            // Database.SetInitializer<Contexto>(new DropCreateDatabaseAlways<Contexto>());
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<ItemFactura> ItemFacturas { get; set; }
        public DbSet<ProgramacionPago> ProgramacionPagos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // modelBuilder.Entity<Cliente>()
            // .HasRequired(d => d.Cliente)
            // .WithMany(w => w.Actividades)
            // .HasForeignKey(d => d.TipoDocumentoId)
            // .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}