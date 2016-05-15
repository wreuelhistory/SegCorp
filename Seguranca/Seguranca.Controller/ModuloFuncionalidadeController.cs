using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguranca.Controller.model;
using Seguranca.Entidades.model;
using Seguranca.Entidades;
using System.Data.Entity;

namespace Seguranca.Controller
{
    public class ModuloFuncionalidadeController : BaseController<ModuloFuncionalidade>
    {
        public ModuloFuncionalidadeController(SegurancaDBContext context) : base(context)
        {
        }

        public override DbSet<ModuloFuncionalidade> DbSet
        {
            get
            {
                return context.moduloFuncionalides;
            }
        }
    }
}
