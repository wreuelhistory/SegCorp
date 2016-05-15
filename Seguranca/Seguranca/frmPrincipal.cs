using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seguranca.utl;

namespace Seguranca
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void módulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UtilForm.abreForm(new frmModulos(), this);
        }

        private void funcionalidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UtilForm.abreForm(new frmFuncionalidade(), this);
        }
    }
}
