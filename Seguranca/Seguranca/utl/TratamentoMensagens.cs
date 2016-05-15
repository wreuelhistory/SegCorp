using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seguranca.utl
{
    public static class TratamentoMensagens
    {
        public static void sucesso (string mensagem)
        {
            MessageBox.Show(mensagem, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void campoBranco(string mensagem)
        {
            MessageBox.Show(mensagem, "Campo em Branco / Não Selecionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
