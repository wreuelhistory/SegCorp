using Seguranca.Entidades.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguranca.Controller.model
{
    public class SDBC
    {
        public SDBC()
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
