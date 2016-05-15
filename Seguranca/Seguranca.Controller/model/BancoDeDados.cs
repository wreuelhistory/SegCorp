using Seguranca.Entidades.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguranca.Controller.model
{
    public class BancoDeDados
    {
        public BancoDeDados()
        { }

        public static SegurancaDBContext Instance
        {
            get
            {
                return SegurancaDBContext.instance();
            }
        }
    }
}
