using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seguranca.Entidades.model;
using Seguranca.Entidades;
using Seguranca.Controller;
using Seguranca.Controller.model;
using Seguranca.utl;

namespace Seguranca
{
    public partial class frmFuncionalidade : Form
    {
        public frmFuncionalidade()
        {
            InitializeComponent();
        }

        ModuloController moduloController = new ModuloController(BancoDeDados.Instance);
        ModuloFuncionalidadeController moduloFuncionalidadeController = new ModuloFuncionalidadeController(BancoDeDados.Instance);
        ModuloFuncionalidade moduloFuncionalidade;
        List<Modulo> listaModulos;
        List<ModuloFuncionalidade> listaFuncionalidade;

        private void frmFuncionalidade_Load(object sender, EventArgs e)
        {
            carregarComboModulo();

        }

        private void carregarComboModulo()
        {
            listaModulos = moduloController.FindAll();
            listaModulos.Add(new Modulo(-1, "Selecione..."));
            listaModulos = listaModulos.OrderBy(l => l.ModuloCodigo).ToList();
            cboModulo.DataSource = listaModulos;
            cboModulo.DisplayMember = "ModuloDescricao";
            cboModulo.ValueMember = "ModuloCodigo";
        }

        private void cboModulo_Leave(object sender, EventArgs e)
        {
            carregarGridView();
        }

        private void carregarGridView()
        {
            int moduloCodigoSelecionado = (int)cboModulo.SelectedValue;
            listaFuncionalidade = moduloFuncionalidadeController.FindAll().Where(m => m.ModuloCodigo == moduloCodigoSelecionado).ToList();
            dgvFuncionalidades.DataSource = listaFuncionalidade;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                int moduloCodigoSelecionado = (int)cboModulo.SelectedValue;
                moduloFuncionalidade = new ModuloFuncionalidade();
                moduloFuncionalidade.ModuloCodigo = moduloCodigoSelecionado;
                moduloFuncionalidade.FuncionalidadeDescricao = txtDescricao.Text.Trim();
                moduloFuncionalidade.FormDescricao = txtFormulario.Text.Trim();
                moduloFuncionalidadeController.Adicionar(moduloFuncionalidade);

                TratamentoMensagens.sucesso("Módulo Inserido com Sucesso");

                carregarGridView();
            }
           
        }

        private bool validarCampos()
        {
            if ((int)cboModulo.SelectedValue  <= 0)
            {
                TratamentoMensagens.campoBranco("Modulo Deveria ser Selecionado");
                cboModulo.Focus();
                return false;
            }

            if (txtDescricao.Text.Trim() == "")
            {
                TratamentoMensagens.campoBranco("Descrição Deveria ser Preenchido");
                txtDescricao.Focus();
                return false;
            }

            if (txtFormulario.Text.Trim() == "")
            {
                TratamentoMensagens.campoBranco("Formulário Deveria ser Preenchido");
                txtFormulario.Focus();
                return false;
            }
            return true;
        }
    }
}
