namespace entifrm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ModuloFuncionalidade")]
    public partial class ModuloFuncionalidade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModuloFuncionalidade()
        {
            GruposUsuarios = new HashSet<GruposUsuarios>();
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        public int ModuloFuncionalidadeCodigo { get; set; }

        public int ModuloCodigo { get; set; }

        [Required]
        [StringLength(200)]
        public string FuncionalidadeDescricao { get; set; }

        [Required]
        [StringLength(200)]
        public string FormDescricao { get; set; }

        [Required]
        [StringLength(200)]
        public string ControleDescricao { get; set; }

        public virtual Modulo Modulo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GruposUsuarios> GruposUsuarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
