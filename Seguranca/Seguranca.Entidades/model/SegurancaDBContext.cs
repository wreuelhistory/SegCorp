using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguranca.Entidades.model
{
    public class SegurancaDBContext : DbContext
    {
        public SegurancaDBContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<SegurancaDBContext>(null);
        }

        public SegurancaDBContext() : base() { }

        public static string GetConnectionString()
        {
            return "Data Source=localhost;Initial Catalog=Seg;Persist Security Info=True;User ID=Agenda;pwd=Agenda#";

        }

        public static SegurancaDBContext Instance()
        {
            SegurancaDBContext databaseConnection = new SegurancaDBContext(GetConnectionString());
            databaseConnection.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            return databaseConnection;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ModuloFuncionalidade>()
            //    .HasMany(m => m.Usuarios)
            //    .WithMany(f => f.moduloFuncionalidades)
            //    .Map(m =>
            //    {
            //        m.MapLeftKey("UsuarioCodigo");
            //        m.MapRightKey("ModuloFuncionalidadeCodigo");
            //        m.ToTable("UsuarioFuncionalidade");
            //    });
        }

        public DbSet<Modulo> modulos { get; set; }
        public DbSet<ModuloFuncionalidade> moduloFuncionalides { get; set; }
        public DbSet<Usuario> usuarios { get; set; }

    }
}
