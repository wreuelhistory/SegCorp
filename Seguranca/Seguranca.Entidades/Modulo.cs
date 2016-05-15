using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Seguranca.Entidades.model;
using System.Collections.Generic;

namespace Seguranca.Entidades
{
    [Table("Modulo")]
    public class Modulo : CustomModel
    {
        [Key]
        public int ModuloCodigo { get; set; }
        public string ModuloDescricao { get; set; }

        public virtual ICollection<ModuloFuncionalidade> ModuloFuncionalidade { get; set; }

        public Modulo()
        {
            ModuloFuncionalidade = new HashSet<ModuloFuncionalidade>();
        }

    }
}

