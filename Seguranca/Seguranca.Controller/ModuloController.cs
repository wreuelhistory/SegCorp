using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguranca.Controller.model;
using Seguranca.Entidades;
using Seguranca.Entidades.model;

namespace Seguranca.Controller
{
    public class ModuloController : BaseController<Modulo>
    {
        public ModuloController(SegurancaDBContext context) : base(context)
        {
        }

        public override DbSet<Modulo> DbSet
        {
            get
            {
                return this.context.modulos;
            }
        }
    }
}
