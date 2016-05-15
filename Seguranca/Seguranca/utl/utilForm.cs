using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seguranca.utl
{
    public static class UtilForm
    {

        public static void abreForm(Form frm, Form mdi = null)
        {
            if (!(mdi == null))
            {
                frm.MdiParent = mdi;
                frm.Show();
            }
            else
            {
                frm.ShowDialog();
            }

            frm.BringToFront();
            frm = null;
        }
       
    }
}
