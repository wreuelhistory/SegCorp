namespace entifrm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Modulo")]
    public partial class Modulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modulo()
        {
            ModuloFuncionalidade = new HashSet<ModuloFuncionalidade>();
        }

        [Key]
        public int ModuloCodigo { get; set; }

        [Required]
        [StringLength(50)]
        public string ModuloDescricao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuloFuncionalidade> ModuloFuncionalidade { get; set; }
    }
}
