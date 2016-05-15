using Seguranca.Entidades.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguranca.Entidades
{
    [Table("Usuario")]
    public class Usuario : CustomModel
    {

        [Key]
        public int UsuarioCodigo { get; set; }

        [Required(ErrorMessage = "Login não informado!")]
        public string UsuarioLogin { get; set; }

        [Required(ErrorMessage = "Senha não informada!")]
        public string UsuarioNome { get; set; }


        //public virtual ICollection<ModuloFuncionalidade> ModuloFuncionalidade { get; set; }

        //public Usuario()
        //{
        //    ModuloFuncionalidade = new HashSet<ModuloFuncionalidade>();
        //}
    }
}
