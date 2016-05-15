using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seguranca.Controller.model;
using Seguranca.Controller;
using Seguranca.Entidades;
using Seguranca.utl;
namespace Seguranca
{
    public partial class frmModulos : Form
    {
        public frmModulos()
        {
            InitializeComponent();
        }

        ModuloController moduloController = new ModuloController(SDBC.Instance);
        Modulo modulo;
        List<Modulo> listaModulos;

        private void frmModulos_Load(object sender, EventArgs e)
        {
            carregarGridView();

        }

        private void carregarGridView()
        {
            listaModulos = new List<Modulo>();
            listaModulos = moduloController.findAll();
            dgvModulos.AutoGenerateColumns = false;
            dgvModulos.DataSource = listaModulos;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            modulo = new Modulo();
            modulo.ModuloDescricao = txtDescricao.Text.Trim();
            moduloController.Adicionar(modulo);
            txtDescricao.Clear();
            TratamentoMensagens.mensagemSucesso("Módulo Inserido com Sucesso");
            carregarGridView();
        }
    }
}
