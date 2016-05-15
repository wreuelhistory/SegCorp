namespace entifrm
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<GruposUsuarios> GruposUsuarios { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<ModuloFuncionalidade> ModuloFuncionalidade { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GruposUsuarios>()
                .Property(e => e.GruposUsuariosDescricao)
                .IsUnicode(false);

            modelBuilder.Entity<GruposUsuarios>()
                .HasMany(e => e.ModuloFuncionalidade)
                .WithMany(e => e.GruposUsuarios)
                .Map(m => m.ToTable("GruposUsuariosFuncionalidade").MapLeftKey("GruposUsuariosCodigo").MapRightKey("ModuloFuncionalidadeCodigo"));

            modelBuilder.Entity<GruposUsuarios>()
                .HasMany(e => e.Usuario)
                .WithMany(e => e.GruposUsuarios)
                .Map(m => m.ToTable("UsuariosGrupos").MapLeftKey("GruposUsuariosCodigo").MapRightKey("UsuarioCodigo"));

            modelBuilder.Entity<Modulo>()
                .Property(e => e.ModuloDescricao)
                .IsUnicode(false);

            modelBuilder.Entity<Modulo>()
                .HasMany(e => e.ModuloFuncionalidade)
                .WithRequired(e => e.Modulo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModuloFuncionalidade>()
                .Property(e => e.FuncionalidadeDescricao)
                .IsUnicode(false);

            modelBuilder.Entity<ModuloFuncionalidade>()
                .Property(e => e.FormDescricao)
                .IsUnicode(false);

            modelBuilder.Entity<ModuloFuncionalidade>()
                .Property(e => e.ControleDescricao)
                .IsUnicode(false);

            modelBuilder.Entity<ModuloFuncionalidade>()
                .HasMany(e => e.Usuario)
                .WithMany(e => e.ModuloFuncionalidade)
                .Map(m => m.ToTable("UsuarioFuncionalidade").MapLeftKey("ModuloFuncionalidadeCodigo").MapRightKey("UsuarioCodigo"));

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UsuarioLogin)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.UsuarioNome)
                .IsUnicode(false);
        }
    }
}
