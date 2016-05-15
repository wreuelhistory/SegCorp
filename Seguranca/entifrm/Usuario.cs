namespace entifrm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            ModuloFuncionalidade = new HashSet<ModuloFuncionalidade>();
            GruposUsuarios = new HashSet<GruposUsuarios>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UsuarioCodigo { get; set; }

        [StringLength(50)]
        public string UsuarioLogin { get; set; }

        [StringLength(200)]
        public string UsuarioNome { get; set; }

        [StringLength(50)]
        public string UsuarioSenha { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuloFuncionalidade> ModuloFuncionalidade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GruposUsuarios> GruposUsuarios { get; set; }
    }
}
