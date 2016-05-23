using Seguranca.Controller;
using Seguranca.Controller.model;
using Seguranca.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurancaComand
{
    public class ModuloOperacoes
    {
        ModuloController moduloController;
        List<Modulo> listaModulos;
        Modulo modulo;

        public ModuloOperacoes() { }

        public void SelecionarOperacao(int opcaoSelecionada)
        {
            switch (opcaoSelecionada)
            {
                case 1:
                    ListarModulos();
                    break;
                case 2:
                    CadastrarModulos();
                    break;
                case 3:
                    EditarModulos();
                    break;
                default:
                    break;
            }

        }

        public void ListarModulos()
        {
            moduloController = new ModuloController(BancoDeDados.Instance);
            listaModulos = new List<Modulo>();
            listaModulos = moduloController.FindAll();
            for (int pos = 0; pos < listaModulos.Count; pos++)
            {
                Console.WriteLine("ID: " + listaModulos[pos].ModuloCodigo.ToString() + " Descricao: " + listaModulos[pos].ModuloDescricao.ToString());
            }
            Console.WriteLine();
            moduloController = null;
            listaModulos = null;
        }

        public void CadastrarModulos()
        {
            modulo = new Modulo();
            moduloController = new ModuloController(BancoDeDados.Instance);
            Console.Write("Digite o nome do Modulo: ");
            modulo.ModuloDescricao = Console.ReadLine();
            moduloController.Adicionar(modulo);
            ListarModulos();
            modulo = null;
            moduloController = null;
        }

        public void EditarModulos()
        {
            int idBusca;
            string descricao;
            Console.WriteLine("Digite o ID do Modulo a ser Editado:");
            idBusca = BuscarModulo();
            modulo = new Modulo();
            moduloController = new ModuloController(BancoDeDados.Instance);
            modulo = moduloController.FindByPk(idBusca);
            while (modulo == null)
            {
                Console.WriteLine("Id Buscado não existe!");
                Console.WriteLine("Digite o ID do Modulo a ser Editado:");
                idBusca = BuscarModulo();
                modulo = moduloController.FindByPk(idBusca);
            }

            Console.WriteLine("Digite a nova Descricao: ");
            descricao = Console.ReadLine().ToString();
            modulo.ModuloDescricao = descricao;
            moduloController.Alterar(modulo);
            Console.WriteLine("Descricao Alterada");
            moduloController = null;
            modulo = null;
            ListarModulos();

        }

        public int BuscarModulo()
        {
            int idBusca;
            while (!int.TryParse(Console.ReadLine(), out idBusca))
            {
                Console.WriteLine("Digite um valor numerico!");
                Console.WriteLine("Digite o ID de um módulo existente:");
            }

            return idBusca;
        }
    }
}
