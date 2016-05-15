using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguranca.Entidades.model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Seguranca.Entidades
{
    [Table("ModuloFuncionalidade")]
    public class ModuloFuncionalidade : CustomModel
    {
        [Key]
        public int ModuloFuncionalidadeCodigo { get; set; }

        public int ModuloCodigo { get; set; }
        public string FuncionalidadeDescricao { get; set; }
        public string FormDescricao { get; set; }
        //public string? ControleDescricao { get; set; }

        public virtual Modulo Modulo { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }

        public ModuloFuncionalidade()
        {
            Usuario = new HashSet<Usuario>();
        }

    }
}
